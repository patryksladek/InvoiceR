using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Commands.Identity.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace InvoiceR.Presentation.Controllers;

[Route("api/Identity")]
[ApiController]
public class IdentityController : Controller
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Register User")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> Post([FromBody] RegisterUserCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }
}
