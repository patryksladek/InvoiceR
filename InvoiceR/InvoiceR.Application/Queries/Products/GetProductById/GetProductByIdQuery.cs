using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Products.GetProductById;

public record GetProductByIdQuery(int Id) : IQuery<ProductDetailDto>;
