namespace XeroApiDemo.Application.Interfaces;

public interface IXeroClient
{
    Task<XeroInvoicesResponse> GetInvoices(Guid accountId, DateTime fromDate, List<XeroInvoiceStatus> statuses = null);
}
