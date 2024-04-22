using InvoiceR.Application.Dto;
using InvoiceR.Application.Queries.Currencies.GetCurrencies;
using InvoiceR.Application.Queries.Units.GetUnits;
using InvoiceR.Application.Queries.VatRates.GetVatRates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace InvoiceR.Presentation.Controllers;

[Route("api/VatRates")]
[ApiController]
public class VatRatesController : Controller
{
    private readonly IMediator _mediator;

    public VatRatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation("Get Vat Rates")]
    [ProducesResponseType(typeof(IEnumerable<VatRateDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetVatRatesQuery());
        return Ok(result);
    }
}