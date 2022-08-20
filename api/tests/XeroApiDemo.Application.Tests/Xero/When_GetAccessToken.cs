using AutoFixture;
using XeroApiDemo.Application.Xero;

namespace XeroApiDemo.Application.Tests.Xero;

public class When_GetAccessToken : XeroHttpTestBase
{
    private XeroTokenResponse _xeroTokenResponse;
    private XeroAccessTokenClient _xeroTokenClient;

    public override void Given()
    {
        _xeroTokenClient = _fixture.Create<XeroAccessTokenClient>();
        MockRoute(
            method: HttpMethod.Post,
            uri: _xeroOptions.IdentityUri,
            responseCode: HttpStatusCode.OK,
            responseContent: "{ \"access_token\": \"123\", \"expires_in\": 1800, \"token_type\": \"Bearer\" }"
        );
    }

    public override async Task WhenAsync() =>
        _xeroTokenResponse = await _xeroTokenClient.GetAccessToken("foo", "bar");

    [Then]
    public void ItShouldDeserializeProperly() =>
        _xeroTokenResponse.Should().BeEquivalentTo(new XeroTokenResponse
        {
            AccessToken = "123",
            ExpiresIn = 1800,
            TokenType = "Bearer"
        });
    
    [Then]
    public void ItShouldCallCorrectEndpoint()
    {
        _requests.Count.Should().Be(1);
        var request = _requests.Single(); 
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be(_xeroOptions.IdentityUri);
    }
    
    [Then]
    public async Task ItShouldComposeProperRequest()
    {
        var request = _requests.Single();
        request.Content?.Headers.ContentType?.MediaType.Should().BeSameAs("application/x-www-form-urlencoded");
        var content = await request.Content?.ReadAsStringAsync()!;
        content.Should().Be("grant_type=client_credentials");
    }

    [Then]
    public void ItShouldCreateCacheEntry() =>
        _cacheMock.Verify(x => x.CreateEntry("XeroAccessToken"), Times.Once);
}
