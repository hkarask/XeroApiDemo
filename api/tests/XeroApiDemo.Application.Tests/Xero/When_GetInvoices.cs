using AutoFixture;
using XeroApiDemo.Application.Data;
using XeroApiDemo.Application.Xero;

namespace XeroApiDemo.Application.Tests.Xero;

public class When_GetInvoices : XeroHttpTestBase
{
    private List<XeroInvoice> _invoices;
    private IXeroClient _xeroClient;

    public override void Given()
    {
        _fixture.Freeze<Mock<IXeroAccessTokenClient>>()
            .Setup(x => x.GetAccessToken(SeedData.DemoUserXeroIntegration.ClientId, SeedData.DemoUserXeroIntegration.ClientSecret))
            .ReturnsAsync(() => new XeroTokenResponse
            {
                AccessToken = "abc"
            });
        
        _xeroClient = _fixture.Create<XeroClient>();
        
        MockRoute(
            method: HttpMethod.Get,
            uri: $"{_xeroOptions.ApiBase}/Invoices?where=Date>DateTime(2021,06,01)&Statuses=Authorised,Paid",
            responseCode: HttpStatusCode.OK,
            responseContent: GetPayload("GET_Invoices.json")
        );
    }

    public override async Task WhenAsync() =>
        _invoices = (await _xeroClient.GetInvoices(SeedData.DemoAccount.Id, new DateTime(2021, 06, 01), new List<XeroInvoiceStatus>
        {
            XeroInvoiceStatus.Authorised, 
            XeroInvoiceStatus.Paid
        })).Invoices;

    [Then]
    public void ItShouldFindCorrectNrOfInvoices() =>
        _invoices.Count.Should().Be(72);
}
