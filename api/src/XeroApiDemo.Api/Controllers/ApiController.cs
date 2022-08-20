using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace XeroApiDemo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public abstract class ApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
