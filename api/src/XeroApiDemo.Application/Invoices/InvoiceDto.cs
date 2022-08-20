namespace XeroApiDemo.Application.Invoices;

public record InvoiceDto : IMapFrom<Invoice>
{
    public Guid Id { get; init; }
    
    public string ContactName { get; init; }

    public DateTime DateIssued { get; init; }

    public decimal Amount { get; init; }

    public decimal AmountDue { get; init; }
}
