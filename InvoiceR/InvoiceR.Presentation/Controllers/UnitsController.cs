using InvoiceR.Application.Dto;
using InvoiceR.Application.Queries.Currencies.GetCurrencies;
using InvoiceR.Application.Queries.Units.GetUnits;
using InvoiceR.Application.Queries.VatRates.GetVatRates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace InvoiceR.Presentation.Controllers;

[Route("api/Units")]
[ApiController]
public class UnitsController : Controller
{
    private readonly IMediator _mediator;

    public UnitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation("Get Units")]
    [ProducesResponseType(typeof(IEnumerable<UnitDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetUnitsQuery());
        return Ok(result);
    }
}