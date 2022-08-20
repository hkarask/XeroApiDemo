using Microsoft.EntityFrameworkCore;

namespace XeroApiDemo.Application.Accounts;

public class InvoicesOverdueCheck : IAccountCheck
{
    private readonly XeroApiDemoContext _db;

    public InvoicesOverdueCheck(XeroApiDemoContext db) =>
        _db = db;

    public async Task<AccountCheckResult> Check(Guid accountId)
    {
        var overDueInvoicesInPast90Days = await _db.AccountInvoicesPast6Months(accountId)
                .Where(x => x.DateIssued.OlderThanDays(90) && x.AmountDue > 0).AnyAsync();

        return overDueInvoicesInPast90Days 
            ? AccountCheckResult.Failed("Has outstanding invoices older than 90 days")
            : AccountCheckResult.Passed();
    }
}
