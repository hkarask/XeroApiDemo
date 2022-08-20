using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace XeroApiDemo.Application.Xero;

public class XeroAccessTokenClient : IXeroAccessTokenClient
{
    private const string AccessTokenCacheKey = "XeroAccessToken";
    
    private readonly ILogger<XeroAccessTokenClient> _logger;
    private readonly IMemoryCache _cache;
    private readonly HttpClient _httpClient;
    private readonly XeroOptions _options;
    
    public XeroAccessTokenClient(ILogger<XeroAccessTokenClient> logger,
        IMemoryCache cache,
        IHttpClientFactory httpClientFactory,
        XeroOptions options)
    {
        _logger = logger;
        _cache = cache;
        _httpClient = httpClientFactory.CreateClient(nameof(XeroAccessTokenClient));
        _options = options;
    }

    public async Task<XeroTokenResponse> GetAccessToken(string clientId, string clientSecret)
    {
        if (_cache.TryGetValue(AccessTokenCacheKey, out XeroTokenResponse tokenResponse))
            return tokenResponse;

        _logger.LogInformation("Getting access token");
        
        var req = new HttpRequestMessage(HttpMethod.Post, _options.IdentityUri)
        {
            Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded")
        }.UseBasicAuth(clientId, clientSecret);

        var result = await _httpClient.SendAndDeserialize<XeroTokenResponse>(req, JsonCasing.Snake);

        const int safeExpirationOffset = 10;
        _cache.Set(AccessTokenCacheKey, result, TimeSpan.FromSeconds(result.ExpiresIn - safeExpirationOffset));

        return result;
    }
}
