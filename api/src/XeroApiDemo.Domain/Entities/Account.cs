using XeroApiDemo.Domain.Common;

namespace XeroApiDemo.Domain.Entities;

public class Account : Entity
{
    public Guid UserId { get; set; }

    public bool IsHealthy { get; set; }

    public string StatusDescription { get; set; }

    public DateTime? LastSyncedOnUtc { get; set; }
}
