using FluentAssertions;
using InvoiceR.Application.Queries.Customers.GetCustomerById;
using InvoiceR.Application.Queries.Products.GetProductById;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using Moq;

namespace InvoiceR.UnitTests.Queries.Products.GetProductById;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductReadOnlyRepository> _productReadOnlyRepository;

    public GetProductByIdQueryHandlerTests()
    {
        _productReadOnlyRepository = new();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository_WhenGetProductByIdQuery()
    {
        // Arrange
        var product = new Product()
        {
            Id = 1,
            Name = "Product 1",
            Type = ProductType.Product,
            Barcode = "978020137962",
            BarcodeType = ProductBarcodeType.EAN13,
            CurrencyId = 1,
            Currency = new Currency()
            {
                Id = 1,
                Name = "Polish zloty",
                Symbol = "PLN",
                IsDefault = true
            },
            UnitId = 1,
            Unit = new Unit()
            {
                Id = 1,
                Code = "pc",
                Description = "piece - the basic unit of quantity"
            },
            VatRateId = 6,
            VatRate = new VatRate()
            {
                Id = 6,
                Symbol = "23%",
                Value = 0.23m
            },
            NetPrice = 100
        };

        _productReadOnlyRepository.Setup(
            x => x.GetByIdWithDetailAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var handler = new GetProductByIdQueryHandler(
            _productReadOnlyRepository.Object);

        // Act
        await handler.Handle(new GetProductByIdQuery(1), default);

        // Assert
        _productReadOnlyRepository.Verify(
           x => x.GetByIdWithDetailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),
           Times.Once
           );
    }

    [Fact]
    public async Task Handle_Should_ReturnProduct_WhenGetProductByIdQuery()
    {
        // Arrange
        var product = new Product()
        {
            Id = 1,
            Name = "Product 1",
            Type = ProductType.Product,
            Barcode = "978020137962",
            BarcodeType = ProductBarcodeType.EAN13,
            CurrencyId = 1,
            Currency = new Currency()
            {
                Id = 1,
                Name = "Polish zloty",
                Symbol = "PLN",
                IsDefault = true
            },
            UnitId = 1,
            Unit = new Unit()
            {
                Id = 1,
                Code = "pc",
                Description = "piece - the basic unit of quantity"
            },
            VatRateId = 6,
            VatRate = new VatRate()
            {
                Id = 6,
                Symbol = "23%",
                Value = 0.23m
            },
            NetPrice = 100
        };

        _productReadOnlyRepository.Setup(
            x => x.GetByIdWithDetailAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var handler = new GetProductByIdQueryHandler(
           _productReadOnlyRepository.Object);

        // Act
        var productDto = await handler.Handle(new GetProductByIdQuery(1), default);

        // Assert
        productDto.Should().NotBeNull();
        productDto.Id.Should().Be(product.Id);
        productDto.Name.Should().Be(product.Name);
        productDto.ProductType.ToString().Should().Be(product.Type.ToString());
        productDto.Barcode.Should().Be(product.Barcode);
        productDto.ProductBarcodeType.ToString().Should().Be(product.BarcodeType.ToString());
        productDto.CurrencyId.Should().Be(product.CurrencyId);
        productDto.UnitId.Should().Be(product.UnitId);
        productDto.VatRateId.Should().Be(product.VatRateId);
        productDto.Price.Should().Be(product.NetPrice);
        
    }
}