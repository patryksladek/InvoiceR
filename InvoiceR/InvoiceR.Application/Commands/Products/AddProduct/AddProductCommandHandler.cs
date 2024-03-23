

using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Commands.Products.AddProduct;

internal class AddProductCommandHandler : ICommandHandler<AddProductCommand, ProductDetailDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDetailDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        bool isAlreadyExistWithSameName = await _productRepository.IsAlreadyExistWithSameNameAsync(request.Name, cancellationToken);
        if (isAlreadyExistWithSameName)
            throw new ProductWithSameNameAlreadyExistsException(request.Name);

        var newProduct = new Product()
        {
            Name = request.Name,
            Type = (ProductType)request.ProductType,
            Barcode = request.Barcode,
            BarcodeType = (ProductBarcodeType)request.ProductBarcodeType,
            CurrencyId = request.CurrencyId,
            UnitId = request.UnitId,
            VatRateId = request.VatRateId,
            NetPrice = request.Price
        };

        _productRepository.Add(newProduct);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productDto = new ProductDetailDto()
        {
            Id = newProduct.Id,
            Name = newProduct.Name,
            ProductType = newProduct.Type.ToString(),
            Barcode = newProduct.Barcode,
            ProductBarcodeType = newProduct.BarcodeType.ToString(),
            CurrencyId = newProduct.CurrencyId,
            UnitId = newProduct.UnitId,
            VatRateId= newProduct.VatRateId,
            Price = newProduct.NetPrice
        };

        return productDto;
    }
}
