using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace XeroApiDemo.Application.Xero;

public class XeroClient : IXeroClient
{
    private const string AccessTokenCacheKey = "XeroAccessToken";
    
    private readonly HttpClient _httpClient;
    private readonly XeroOptions _options;
    private readonly IMemoryCache _cache;
    private readonly ILogger<XeroClient> _logger;

    public XeroClient(IHttpClientFactory httpClientFactory,
        XeroOptions options,
        IMemoryCache cache,
        ILogger<XeroClient> logger)
    {
        _cache = cache;
        _options = options;
        _httpClient = httpClientFactory.CreateClient(nameof(XeroClient));
        _logger = logger;
    }

    public async Task<List<Invoice>> GetInvoices(DateOnly fromDate, List<InvoiceStatus> statuses = null)
    {
        var uri = $"{_options.ApiBase}/Invoices?where=Date>({fromDate.ToString("yyyy,MM,dd")})";
        
        if (statuses != null) 
            uri += $"&Statuses={string.Join(',', statuses)}";

        var accessToken = await GetAccessToken();
        var req = new HttpRequestMessage(HttpMethod.Get, uri)
            .UseBearerToken(accessToken.AccessToken);

        var res = await _httpClient.SendAndDeserialize<InvoicesResponse>(req);
        if (!res.IsSuccessful)
            throw new IntegrationException($"Invalid status for Invoices response: {res.Status}");

        return res.Invoices;
    }

    public async Task<TokenResponse> GetAccessToken()
    {
        if (_cache.TryGetValue(AccessTokenCacheKey, out TokenResponse tokenResponse))
            return tokenResponse;

        _logger.LogInformation("Getting access token");
        
        var req = new HttpRequestMessage(HttpMethod.Post, _options.IdentityUri)
        {
            Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded")
        }.UseBasicAuth(_options.ClientId, _options.ClientSecret);

        var token = await _httpClient.SendAndDeserialize<TokenResponse>(req, JsonCasing.Snake);
        
        const int safeExpirationOffset = 10;
        _cache.Set(AccessTokenCacheKey, token, TimeSpan.FromSeconds(token.ExpiresIn - safeExpirationOffset));

        return token;
    }
}
