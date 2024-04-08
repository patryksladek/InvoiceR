using InvoiceR.Application.Queries.Currencies.GetCurrencies;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Definitions;
using Moq;

namespace InvoiceR.UnitTests.Queries.Currencies.GetCurrencies;

public class GetCurrenciesQueryHandlerTests
{
    private readonly Mock<ICurrencyReadOnlyRepository> _currencyReadOnlyRepository;

    public GetCurrenciesQueryHandlerTests()
    {
        _currencyReadOnlyRepository = new();
    }

    [Fact]
    public void Handle_Should_CallGetAllAsyncOnRepository_WhenGetCurrenciesQuery()
    {
        // Arrange
        var currenciesQuery = new List<Currency>().AsQueryable();

        _currencyReadOnlyRepository.Setup(
            x => x.GetAllAsync()).Returns(currenciesQuery);

        var handler = new GetCurrenciesQueryHandler(
            _currencyReadOnlyRepository.Object);

        // Act
        handler.Handle(new GetCurrenciesQuery(), default);

        // Assert
        _currencyReadOnlyRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}