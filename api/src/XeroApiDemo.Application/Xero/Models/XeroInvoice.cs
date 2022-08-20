namespace XeroApiDemo.Application.Xero.Models;

public class XeroInvoice
{
    public XeroInvoiceStatus Status { get; set; }

    public string Type { get; set; }
        
    public XeroContact XeroContact { get; set; }

    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    public decimal AmountDue { get; set; }
}
