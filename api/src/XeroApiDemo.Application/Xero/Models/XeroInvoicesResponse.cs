using Newtonsoft.Json;

namespace XeroApiDemo.Application.Xero.Models;

public class XeroInvoicesResponse
{
    public DateTime DateTimeUtc { get; set; }

    /// <summary>
    /// <example>OK</example>
    /// </summary>
    public string Status { get; set; }

    public bool IsSuccessful => Status == "OK";

    public List<XeroInvoice> Invoices { get; set; }

    [JsonIgnore]
    public string Payload { get; set; }
}
