using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
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
        var products = _productReadOnlyRepository.GetAllAsync();

        var customersDto = await products.Select(x => new ProductDto()
        {
            Id = x.Id,
            Name = x.Name,
            Barcode = x.Barcode,
            Price = x.NetPrice,
            Currency = x.Currency.Symbol
        })
        .ToListAsync();

        return customersDto;
    }
}
