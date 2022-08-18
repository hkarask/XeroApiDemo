using System.Collections.ObjectModel;

namespace XeroApiDemo.Application.Xero.Models;

public class InvoicesResponse
{
    public DateTime DateTimeUtc { get; set; }

    /// <summary>
    /// <example>OK</example>
    /// </summary>
    public string Status { get; set; }

    public bool IsSuccessful => Status == "OK";

    public List<Invoice> Invoices { get; set; }
}
