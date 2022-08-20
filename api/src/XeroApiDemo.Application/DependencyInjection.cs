using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace XeroApiDemo.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(executingAssembly);
        services.AddMediatR(executingAssembly);
    }
}
