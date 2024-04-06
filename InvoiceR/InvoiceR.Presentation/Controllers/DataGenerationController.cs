using InvoiceR.Application.Commands.DataGeneration;
using InvoiceR.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace InvoiceR.Presentation.Controllers;

[Route("api/DataGeneration")]
[ApiController]
public class DataGenerationController : Controller
{
    private readonly IMediator _mediator;

    public DataGenerationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation("Generate Data")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> Post([FromBody] GenerateDateCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }
}
