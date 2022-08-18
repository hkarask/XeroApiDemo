namespace XeroApiDemo.Application.Xero.Models;

public class XeroOptions
{
    public const string Key = "Xero";
    
    public string ApiBase { get; set; }
    
    public string IdentityUri { get; set; }
    
    public string ClientId { get; set; }
    
    public string ClientSecret { get; set; }
}
