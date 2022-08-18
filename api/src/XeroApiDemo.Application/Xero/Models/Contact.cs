namespace XeroApiDemo.Application.Xero.Models;

public class Contact
{
    public string ContactId { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public string CurrencyCode { get; set; }

    public decimal Total { get; set; }

    public decimal AmountDue { get; set; }
}
