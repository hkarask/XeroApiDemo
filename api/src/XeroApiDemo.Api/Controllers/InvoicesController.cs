using Microsoft.AspNetCore.Mvc;
using XeroApiDemo.Application.Invoices;

namespace XeroApiDemo.Api.Controllers;

public class InvoicesController : ApiController
{
    /// <summary>
    /// Account's latest invoices
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<InvoiceDto>> GetInvoices() =>
        Mediator.Send(new GetInvoicesQuery{ AccountId = SeedData.DemoAccount.Id });
}
