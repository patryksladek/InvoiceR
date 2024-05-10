using InvoiceR.Application.Commands.DataGeneration;
using InvoiceR.Domain.Abstractions.Generator;
using InvoiceR.Domain.Exceptions;
using Moq;

namespace InvoiceR.UnitTests.Commands.DataGeneration.GenerateDate;

public class GenerateDateCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowNotEmptyDatabaseException_WhenDatabaseNotEmpty()
    {
        // Arrange
        var command = new GenerateDateCommand(10, 5, 3);

        var dataGeneratorMock = new Mock<IDataGenerator>();
        dataGeneratorMock.Setup(m => m.IsNoData()).Returns(false);

        var handler = new GenerateDateCommandHandler(dataGeneratorMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotEmptyDatabaseException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_CallGenerateDataMethod_WhenDatabaseEmpty()
    {
        // Arrange
        var command = new GenerateDateCommand(10, 5, 3);

        var dataGeneratorMock = new Mock<IDataGenerator>();
        dataGeneratorMock.Setup(m => m.IsNoData()).Returns(true);

        var handler = new GenerateDateCommandHandler(dataGeneratorMock.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        dataGeneratorMock.Verify(m => m.GenerateData(command.CustomersCount, command.ProductsCount, command.InvoicesCount), Times.Once);
    }
}
