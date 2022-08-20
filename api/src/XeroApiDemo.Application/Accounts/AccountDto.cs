namespace XeroApiDemo.Application.Accounts;

public record AccountDto : IMapFrom<Account>
{
    public bool IsHealthy { get; init; }

    public string StatusDescription { get; init; }
}
