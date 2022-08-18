using System.Net.Http.Headers;

namespace XeroApiDemo.Application.Extensions;

public static class HttpExtensions
{
    public static HttpRequestMessage UseBasicAuth(this HttpRequestMessage request, string user, string secret)
    {
        var credentialBytes = Encoding.ASCII.GetBytes($"{user}:{secret}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentialBytes));
        return request;
    }
    
    public static HttpRequestMessage UseBearerToken(this HttpRequestMessage request, string token)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return request;
    }

    public static async Task<TResponse> SendAndDeserialize<TResponse>(this HttpClient httpClient,
        HttpRequestMessage request,
        JsonCasing jsonCasing = JsonCasing.Default)
    {
        var res = await httpClient.SendAsync(request);
        var content = await res.Content.ReadAsStringAsync();

        if (!res.IsSuccessStatusCode)
            throw new IntegrationException($"Request to '{request.RequestUri}' failed ({res.StatusCode}): {content}");

        return content.Deserialize<TResponse>(jsonCasing);
    }
}
