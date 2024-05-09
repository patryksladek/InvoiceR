using InvoiceR.Application.Queries.Products.GetProducts;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Products;
using Moq;

namespace InvoiceR.UnitTests.Queries.Products.GetProducts;

public class GetProductsHandlerTests
{
    private readonly Mock<IProductReadOnlyRepository> _productReadOnlyRepository;

    public GetProductsHandlerTests()
    {
        _productReadOnlyRepository = new();
    }

    [Fact]
    public void Handle_Should_CallGetAllAsyncOnRepository_WhenGetProductsQuery()
    {
        // Arrange
        var productsQuery = new List<Product>().AsQueryable();

        _productReadOnlyRepository.Setup(
            x => x.GetAllAsync()).Returns(productsQuery);

        var handler = new GetProductsQueryHandler(
            _productReadOnlyRepository.Object);

        // Act
        handler.Handle(new GetProductsQuery(), default);

        // Assert
        _productReadOnlyRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}