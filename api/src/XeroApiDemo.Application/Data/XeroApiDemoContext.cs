using Microsoft.EntityFrameworkCore;

namespace XeroApiDemo.Application.Data;

public class XeroApiDemoContext : DbContext
{
    public XeroApiDemoContext(DbContextOptions<XeroApiDemoContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Integration> Integrations { get; set; }

    public DbSet<Invoice> Invoices { get; set; }
    public IQueryable<Invoice> AccountInvoicesPast6Months(Guid accountId) => 
        Invoices.Where(x => x.AccountId == accountId && x.DateIssued > DateTime.Now.AddMonths(-6));


    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(SeedData.DemoUser);
        modelBuilder.Entity<Account>().HasData(SeedData.DemoAccount);
        modelBuilder.Entity<Integration>().HasData(SeedData.DemoUserXeroIntegration);
    }
}
