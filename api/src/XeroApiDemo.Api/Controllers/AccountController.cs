using Microsoft.AspNetCore.Mvc;
using XeroApiDemo.Application.Accounts;

namespace XeroApiDemo.Api.Controllers;

public class AccountController : ApiController
{
    /// <summary>
    /// Basic account information
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<AccountDto> GetAccount() =>
        Mediator.Send(new GetAccountQuery{ AccountId = SeedData.DemoAccount.Id });

}
