using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Customers;
using Mapster;

namespace InvoiceR.Application.Queries.Products.GetProductById;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDetailDto>
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;

    public GetProductByIdQueryHandler(IProductReadOnlyRepository productReadOnlyRepository)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
    }

    public async Task<ProductDetailDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productReadOnlyRepository.GetByIdWithDetailAsync(request.Id);

        var productDto = product.Adapt<ProductDetailDto>();

        return productDto;
    }
}
