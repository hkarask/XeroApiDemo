using XeroApiDemo.Domain.Common;

namespace XeroApiDemo.Domain.Entities;

public class Invoice : Entity
{
    public Guid AccountId { get; set; }

    public string ContactName { get; set; }

    public DateTime DateIssued { get; set; }

    public decimal Amount { get; set; }

    public decimal AmountDue { get; set; }
}
