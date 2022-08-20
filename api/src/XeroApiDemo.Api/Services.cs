using XeroApiDemo.Application.Accounts;
using XeroApiDemo.Application.Xero;

namespace XeroApiDemo.Api;

public static class Services
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IXeroAccessTokenClient, XeroAccessTokenClient>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IXeroClient, XeroClient>();
        services.AddTransient<IAccountCheck, InvoicesOverdueCheck>();
        services.AddTransient<IAccountCheck, InvoicesSumCheck>();
    }
}
