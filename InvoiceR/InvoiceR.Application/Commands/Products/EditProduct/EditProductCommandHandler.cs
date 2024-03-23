using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Enums;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Commands.Products.EditProduct;

internal class EditProductCommandHandler : ICommandHandler<EditProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new ProductNotFoundException(request.Id);

        bool isAlreadyExistWithSameName = await _productRepository.IsAlreadyExistWithSameNameAsync(request.Id, request.Name, cancellationToken);
        if (isAlreadyExistWithSameName)
            throw new ProductWithSameNameAlreadyExistsException(request.Name);

        product.Name = request.Name;
        product.Type = (ProductType)request.ProductType;
        product.Barcode = request.Barcode;
        product.BarcodeType = (ProductBarcodeType)request.ProductBarcodeType;
        product.CurrencyId = request.CurrencyId;
        product.UnitId = request.UnitId;
        product.VatRateId = request.VatRateId;
        product.NetPrice = request.Price;

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
