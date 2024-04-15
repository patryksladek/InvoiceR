using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Commands.Customers.EditCusotmer;
using InvoiceR.Application.Commands.Customers.RemoveCustomer;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Queries.Customers.ExportCustomers;
using InvoiceR.Application.Queries.Customers.GetCustomerById;
using InvoiceR.Application.Queries.Customers.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace InvoiceR.Presentation.Controllers;

[Route("api/Customers")]
[ApiController]
public class CustomersController : Controller
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation("Get Customers")]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetCustomersQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get Customer by ID")]
    [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("{exportType}/{ids}")]
    [SwaggerOperation("Export Customers")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Export([FromRoute] ExportTypeDto exportType, [FromRoute] int[] ids)
    {
        var result = await _mediator.Send(new ExportCustomersQuery(ids, exportType));
        return File(result.Content, MediaTypeNames.Application.Octet, result.Name);
    }

    [HttpPost]
    [SwaggerOperation("Add Customer")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> Post([FromBody] AddCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [SwaggerOperation("Edit Customer")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Put([FromBody] EditCustomerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Remove Customer")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new RemoveCustomerCommand(id));
        return NoContent();
    }
}