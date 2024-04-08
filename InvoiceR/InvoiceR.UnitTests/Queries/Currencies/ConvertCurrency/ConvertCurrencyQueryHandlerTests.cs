using InvoiceR.Application.Queries.Currencies.ConvertCurrency;
using InvoiceR.Domain.Abstractions;
using Moq;

namespace InvoiceR.UnitTests.Queries.Currencies.ConvertCurrency;

public class ConvertCurrencyQueryHandlerTests
{
    private readonly Mock<ICurrencyConverter> _currencyConverter;

    public ConvertCurrencyQueryHandlerTests()
    {
        _currencyConverter = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnsConvertedAmount_WhenValidRequest()
    {
        // Arrange
        _currencyConverter.Setup(m => m.Convert(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                             .Returns(10); 

        var handler = new ConvertCurrencyQueryHandler(_currencyConverter.Object);
        var query = new ConvertCurrencyQuery("USD", "EUR", DateTime.Now, 5);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result); 
    }

    [Fact]
    public async Task Handle_Should_ThrowsInvalidOperationException_WhenInvalidCurrencySymbols()
    {
        // Arrange
        _currencyConverter.Setup(m => m.Convert(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                             .Throws<InvalidOperationException>(); 

        var handler = new ConvertCurrencyQueryHandler(_currencyConverter.Object);
        var query = new ConvertCurrencyQuery("XXX", "EUR", DateTime.Now, 5);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(query, CancellationToken.None));
    }
}