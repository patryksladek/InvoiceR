using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using MediatR;

namespace InvoiceR.Application.Queries.Products.GetProducts;

public record GetProductsQuery() : IQuery<IReadOnlyCollection<ProductDto>>;