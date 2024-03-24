using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Commands.Customers.EditCusotmer;
using InvoiceR.Application.Commands.Customers.RemoveCustomer;
using InvoiceR.Application.Commands.Products.AddProduct;
using InvoiceR.Application.Commands.Products.EditProduct;
using InvoiceR.Application.Commands.Products.RemoveProduct;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Queries.Customers.GetCustomerById;
using InvoiceR.Application.Queries.Customers.GetCustomers;
using InvoiceR.Application.Queries.Products.GetProductById;
using InvoiceR.Application.Queries.Products.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace InvoiceR.Presentation.Controllers;

[Route("api/Products")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation("Get Products")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get()
    {
        var result = await _mediator.Send(new GetProductsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get Product by ID")]
    [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [SwaggerOperation("Add Product")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> Post([FromBody] AddProductCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [SwaggerOperation("Edit Product")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Put([FromBody] EditProductCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Remove Product")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new RemoveProductCommand(id));
        return NoContent();
    }
}