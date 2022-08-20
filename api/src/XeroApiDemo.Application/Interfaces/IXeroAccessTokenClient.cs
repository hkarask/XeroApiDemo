namespace XeroApiDemo.Application.Interfaces;

public interface IXeroAccessTokenClient
{
    Task<XeroTokenResponse> GetAccessToken(string clientId, string clientSecret);
}
