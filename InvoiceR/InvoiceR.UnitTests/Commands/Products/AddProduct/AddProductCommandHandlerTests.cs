using InvoiceR.Application.Commands.Products.AddProduct;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using InvoiceR.Domain.Exceptions;
using Moq;

namespace InvoiceR.UnitTests.Commands.Customers.AddCustomer;


public class AddProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AddProductCommandHandlerTests()
    {
        _productRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallAddOnReposiotry_WhenNameIsUnique()
    {
        // Arrange
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            ProductType = (int)ProductType.Product,
            Barcode = "978020137962",
            ProductBarcodeType = (int)ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            Price = 100
        };

        _productRepositoryMock.Setup(
            x => x.Add(It.IsAny<Product>()));

        _productRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new AddProductCommandHandler(
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act 
        var productDto = await handler.Handle(command, default);

        // Assert
        _productRepositoryMock.Verify(
            x => x.Add(It.Is<Product>(x => x.Id == productDto.Id)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrowProductAlreadyExistsException_WhenNameIsNotUnique()
    {
        // Arrange
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            ProductType = (int)ProductType.Product,
            Barcode = "978020137962",
            ProductBarcodeType = (int)ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            Price = 100
        };

        _productRepositoryMock.Setup(
             x => x.Add(It.IsAny<Product>()));

        _productRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new AddProductCommandHandler(
           _productRepositoryMock.Object,
           _unitOfWorkMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ProductWithSameNameAlreadyExistsException>(async () => await handler.Handle(command, default));
    }
}
