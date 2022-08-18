namespace XeroApiDemo.Application.Tests.Xero;

public class WhenGettingAccessToken : XeroHttpTestBase
{
    private TokenResponse _tokenResponse;

    public override void Given() =>
        MockRoute(
            method: HttpMethod.Post, 
            uri: _xeroOptions.IdentityUri, 
            responseCode: HttpStatusCode.OK, 
            responseContent: "{ \"access_token\": \"123\", \"expires_in\": 1800, \"token_type\": \"Bearer\" }"
        );

    public override async Task WhenAsync() =>
        _tokenResponse = await _xeroClient.GetAccessToken();

    [Then]
    public void ItShouldDeserializeProperly() =>
        _tokenResponse.Should().BeEquivalentTo(new TokenResponse
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
}
