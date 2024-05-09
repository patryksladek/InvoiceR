using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Products.GetProducts;

public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IReadOnlyCollection<ProductDto>>
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;

    public GetProductsQueryHandler(IProductReadOnlyRepository productReadOnlyRepository)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
    }

    public async Task<IReadOnlyCollection<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productReadOnlyRepository.GetAllAsync().ToListAsync();

        var productsDto = products.Adapt<IReadOnlyCollection<ProductDto>>();

        return productsDto;
    }
}
