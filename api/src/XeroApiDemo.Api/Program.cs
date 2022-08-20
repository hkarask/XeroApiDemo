using System.Reflection;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.OpenApi.Models;
using XeroApiDemo.Application;
using XeroApiDemo.Application.Xero.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddApplication();
services.AddHttpClient();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Xero Demo API", 
        Version = "v1"
    });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "XeroApiDemo.Api.xml"));
});
services.AddSingleton(builder.Configuration.GetSection(XeroOptions.Key).Get<XeroOptions>());
services.AddDbContext<XeroApiDemoContext>();
services.AddHangfire(conf => conf.UseMemoryStorage());
services.AddHangfireServer();
services.AddMemoryCache();
services.AddResponseCompression();
services.AddHealthChecks();

services.AddServices();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<XeroApiDemoContext>().Database.EnsureCreated();
}

app.UseHangfireDashboard();

BackgroundJob.Enqueue<IAccountService>(x => x.UpdateAccounts());
RecurringJob.AddOrUpdate<IAccountService>("SyncInvoices", x => x.UpdateAccounts(), Cron.Daily);

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
