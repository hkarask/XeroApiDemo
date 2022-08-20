using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq.Protected;
using NUnit.Framework;
using XeroApiDemo.Application.Data;
using XeroApiDemo.Application.Xero;

namespace XeroApiDemo.Application.Tests.Xero;

public abstract class XeroHttpTestBase : TestSpecification
{
    protected readonly IFixture _fixture;
    protected readonly XeroOptions _xeroOptions;
    protected readonly Mock<HttpMessageHandler> _handlerMock;
    protected readonly Mock<IMemoryCache> _cacheMock;
    protected readonly List<HttpRequestMessage> _requests = new();

    protected XeroHttpTestBase()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        var dbOptions = new DbContextOptionsBuilder<XeroApiDemoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var db = new XeroApiDemoContext(dbOptions);
        db.Database.EnsureCreated();
            
        _fixture.Inject(db);
        _cacheMock = _fixture.Freeze<Mock<IMemoryCache>>();
        
        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClientFactoryMock = _fixture.Freeze<Mock<IHttpClientFactory>>();
        httpClientFactoryMock
            .Setup(x => x.CreateClient(nameof(XeroClient)))
            .Returns(new HttpClient(_handlerMock.Object));
        httpClientFactoryMock
            .Setup(x => x.CreateClient(nameof(XeroAccessTokenClient)))
            .Returns(new HttpClient(_handlerMock.Object));
        
        _xeroOptions = new XeroOptions
        {
            IdentityUri = "http://id.xero.local", 
            ApiBase = "http://api.xero.local"
        };
        _fixture.Inject(_xeroOptions);
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

    protected string GetPayload(string fileName) => 
        File.ReadAllText($"{TestContext.CurrentContext.TestDirectory}/Xero/payloads/{fileName}");
}
