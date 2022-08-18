namespace XeroApiDemo.Application.Xero.Models;

public class Invoice
{
    public InvoiceStatus Status { get; set; }

    public string Type { get; set; }
        
    public Contact Contact { get; set; }

    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    public decimal AmountDue { get; set; }
}
