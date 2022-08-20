using XeroApiDemo.Domain.Common;
using XeroApiDemo.Domain.Enums;

namespace XeroApiDemo.Domain.Entities;

public class Integration : Entity
{
    public Guid AccountId { get; set; }

    public Account Account { get; set; }

    public IntegrationType Type { get; set; }

    public string ClientId { get; set; }
    
    // Should be encrypted!
    public string ClientSecret { get; set; }
}
