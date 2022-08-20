namespace XeroApiDemo.Application.Xero;

public class XeroClient : IXeroClient
{
    private readonly HttpClient _httpClient;
    private readonly XeroOptions _options;
    private readonly IXeroAccessTokenClient _tokenClient;
    private readonly XeroApiDemoContext _db;

    public XeroClient(IHttpClientFactory httpClientFactory,
        XeroOptions options,
        IXeroAccessTokenClient tokenClient,
        XeroApiDemoContext db)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient(nameof(XeroClient));
        _tokenClient = tokenClient;
        _db = db;
    }

    public async Task<XeroInvoicesResponse> GetInvoices(Guid accountId, DateTime fromDate, List<XeroInvoiceStatus> statuses = null)
    {
        var uri = $"{_options.ApiBase}/Invoices?where=Date>DateTime({fromDate:yyyy,MM,dd})";
        
        if (statuses != null) 
            uri += $"&Statuses={string.Join(',', statuses)}";

        var credentials = _db.Integrations.SingleOrDefault(x => x.AccountId == accountId && x.Type == IntegrationType.Xero) ??
                    throw new InvalidOperationException("No Xero integrations found");
        
        var accessToken = await _tokenClient.GetAccessToken(credentials.ClientId, credentials.ClientSecret);
        var req = new HttpRequestMessage(HttpMethod.Get, uri)
            .UseBearerToken(accessToken.AccessToken);

        var result = await _httpClient.SendAndDeserialize<XeroInvoicesResponse>(req);
        if (!result.IsSuccessful)
            throw new IntegrationException($"Invalid status for Invoices response: {result.Status}");
        
        return result;
    }
}
