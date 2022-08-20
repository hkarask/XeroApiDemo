using Microsoft.EntityFrameworkCore;

namespace XeroApiDemo.Application.Accounts;

public class AccountService : IAccountService
{
    private readonly XeroApiDemoContext _db;
    private readonly IXeroClient _xeroClient;
    private readonly IEnumerable<IAccountCheck> _accountChecks;

    public AccountService(XeroApiDemoContext db, IXeroClient xeroClient, IEnumerable<IAccountCheck> accountChecks)
    {
        _db = db;
        _xeroClient = xeroClient;
        _accountChecks = accountChecks;
    }

    public async Task UpdateAccounts()
    {
        var sixMonthsBack = DateTime.UtcNow.AddMonths(-6);
        
        var accountIdsToBeSynced = await _db.Accounts.Where(x => x.LastSyncedOnUtc.GetValueOrDefault(DateTime.MinValue) < sixMonthsBack)
            .Select(x => x.Id).ToListAsync();    
        
        // Ideally ran in batches and parallel, depending on Xero throttling
        foreach (var accountId in accountIdsToBeSynced) 
            await UpdateAccount(accountId);
    }

    private async Task UpdateAccount(Guid accountId)
    {
        var invoices = await FetchInvoices(accountId);
        await _db.Invoices.AddRangeAsync(invoices);
        await _db.SaveChangesAsync();

        var failedChecks = await GetFailedChecks(accountId);

        var account = await _db.Accounts.SingleOrDefaultAsync<Account>(x => x.Id == accountId) ??
                      throw new InvalidOperationException($"Account {accountId} not found");
        
        account.IsHealthy = !failedChecks.Any();
        account.StatusDescription = string.Join(',', failedChecks);

        await _db.SaveChangesAsync();
    }

    private async Task<List<Invoice>> FetchInvoices(Guid accountId)
    {
        var sixMonthsBack = DateTime.UtcNow.AddMonths(-6);
        var result = await _xeroClient.GetInvoices(
            accountId,
            sixMonthsBack,
            new List<XeroInvoiceStatus> { XeroInvoiceStatus.Paid, XeroInvoiceStatus.Authorised }
        );

        // Could be done using AutoMapper
        return result.Invoices.Select(x => new Invoice
        {
            AccountId = accountId,
            Amount = x.Total,
            AmountDue = x.AmountDue,
            ContactName = x.Contact.Name,
            DateIssued = x.Date
        }).ToList();
    }
    
    private async Task<List<string>> GetFailedChecks(Guid accountId)
    {
        var tasks = _accountChecks
            .Select(x => x.Check(accountId))
            .ToList();

        return (await Task.WhenAll(tasks))
            .Where(x => !x.IsValid).Select(t => t.Message)
            .ToList();
    }
}
