using XeroApiDemo.Domain.Enums;

namespace XeroApiDemo.Application.Data;

public static class SeedData
{
    public static readonly User DemoUser = new()
    {
        Id = new Guid("1EF30A75-249E-491F-B750-2BD5E66D6F68"),
        Email = "john@smith.com"
    };

    public static readonly Account DemoAccount = new()
    {
        Id = new Guid("C0266FE5-937D-4443-929F-E821FC53FF79"),
        UserId = DemoUser.Id,
        IsHealthy = false
    };

    public static readonly Integration DemoUserXeroIntegration = new()
    {
        Id = new Guid("3825B016-ADB3-4674-A4C7-57C59B4B638A"),
        Type = IntegrationType.Xero,
        AccountId = DemoAccount.Id,
        ClientId = "A315A91638384819A8CF8633B36EB065",
        ClientSecret = "qeBykcoftjSMnpFSUGlXH-N0xVDaSfbd8B7PNbrRR5b50FaH"
    };
}
