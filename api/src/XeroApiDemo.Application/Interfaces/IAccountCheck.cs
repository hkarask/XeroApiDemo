using XeroApiDemo.Application.Accounts;

namespace XeroApiDemo.Application.Interfaces;

public interface IAccountCheck
{
    public Task<AccountCheckResult> Check(Guid accountId);
}
