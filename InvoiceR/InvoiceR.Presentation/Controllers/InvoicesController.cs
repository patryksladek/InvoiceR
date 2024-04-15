using InvoiceR.Application.Commands.Invoices.AddInvoice;
using InvoiceR.Application.Commands.Invoices.EditInvoice;
using InvoiceR.Application.Commands.Invoices.RemoveInvoice;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Queries.Customers.ExportCustomers;
using InvoiceR.Application.Queries.Invoices.GetInvoiceById;
using InvoiceR.Application.Queries.Invoices.GetInvoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace InvoiceR.Presentation.Controllers;

[Route("api/Invoices")]
[ApiController]
public class InvoicesController : Controller
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation("Get Invoices")]
    [ProducesResponseType(typeof(IEnumerable<InvoiceDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetInvoicesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get Invoice by ID")]
    [ProducesResponseType(typeof(InvoiceDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetInvoiceByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("{exportType}/{ids}")]
    [SwaggerOperation("Export Invoices")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Export([FromRoute] ExportTypeDto exportType, [FromRoute] int[] ids)
    {
        var result = await _mediator.Send(new ExportInvoicesQuery(ids, exportType));
        return File(result.Content, MediaTypeNames.Application.Octet, result.Name);
    }

    [HttpPost]
    [SwaggerOperation("Add Invoice")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> Post([FromBody] AddInvoiceCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [SwaggerOperation("Edit Invoice")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Put([FromBody] EditInvoiceCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Remove Invoice")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new RemoveInvoiceCommand(id));
        return NoContent();
    }
}