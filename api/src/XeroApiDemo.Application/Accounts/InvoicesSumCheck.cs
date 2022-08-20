using Microsoft.EntityFrameworkCore;

namespace XeroApiDemo.Application.Accounts;

public class InvoicesSumCheck : IAccountCheck
{
    private readonly XeroApiDemoContext _db;

    public InvoicesSumCheck(XeroApiDemoContext db) =>
        _db = db;

    public async Task<AccountCheckResult> Check(Guid accountId)
    {
        const int targetInvoiceSum = 100000;
        var sumOfInvoices = await _db.AccountInvoicesPast6Months(accountId).SumAsync(x => x.Amount);

        return sumOfInvoices <= targetInvoiceSum
            ? AccountCheckResult.Failed($"Sum of invoices ({sumOfInvoices}) not greater than {targetInvoiceSum}")
            : AccountCheckResult.Passed();
    }
}
