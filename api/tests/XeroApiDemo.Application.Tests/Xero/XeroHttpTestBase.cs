using AutoFixture;
using AutoFixture.AutoMoq;
using Moq.Protected;
using XeroApiDemo.Application.Xero;

namespace XeroApiDemo.Application.Tests.Xero;

public abstract class XeroHttpTestBase : TestSpecification
{
    protected readonly IFixture _fixture;
    protected readonly XeroOptions _xeroOptions;
    protected readonly XeroClient _xeroClient;
    protected readonly Mock<HttpMessageHandler> _handlerMock;
    protected readonly List<HttpRequestMessage> _requests = new();

    protected XeroHttpTestBase()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        
        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClientFactoryMock = _fixture.Freeze<Mock<IHttpClientFactory>>();
        httpClientFactoryMock
            .Setup(x => x.CreateClient(nameof(XeroClient)))
            .Returns(new HttpClient(_handlerMock.Object));
        
        _xeroOptions = new XeroOptions
        {
            IdentityUri = "http://id.xero.local", 
            ApiBase = "http://api.xero.local", 
            ClientId = "123", 
            ClientSecret = "456"
        };
        _fixture.Inject(_xeroOptions);
        _xeroClient = _fixture.Create<XeroClient>();
    }

    protected void MockRoute(HttpMethod method, string uri, HttpStatusCode responseCode, string responseContent)
    {
        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.Method == method && x.RequestUri == new Uri(uri)),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback<HttpRequestMessage, CancellationToken>((message, _) =>
            {
                _requests.Add(message);
            })
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = responseCode, 
                Content = new StringContent(responseContent)
            })
            .Verifiable();
    }

    protected void VerifyHttpMethodCall(Times times, HttpMethod method, string uri) =>
        _handlerMock
            .Protected()
            .Verify("SendAsync",
                times,
                ItExpr.Is<HttpRequestMessage>(req => req.Method == method && req.RequestUri == new Uri(uri)),
                ItExpr.IsAny<CancellationToken>()
            );
}
