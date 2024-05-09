using InvoiceR.Application.Commands.Invoices.AddInvoice;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Invoices;
using Moq;

namespace InvoiceR.UnitTests.Commands.Invoices.AddInvoice;


public class AddInvoiceCommandHandlerTests
{
    private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AddInvoiceCommandHandlerTests()
    {
        _invoiceRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallAddOnReposiotry_WhenAddInvoice()
    {
        // Arrange
        var command = new AddInvoiceCommand()
        {
            Date = DateOnly.Parse("2024-04-03"),
            CustomerId = 1,
            Description = "Description",
            Net = 1000,
            Vat = 230,
            Gross = 1230,
            CurrencyId = 1,
            InvoiceItems = new List<AddInvoiceItemCommand>()
            {
                new AddInvoiceItemCommand()
                {
                    OrdinalNumber = 1,
                    ProductId = 1,
                    Quantity = 1,
                    Price = 1000,
                    Net = 1000,
                    Gross = 1230,
                    CurrencyId = 1,
                    VatRateId = 6
                }
            }
        };

        _invoiceRepositoryMock.Setup(
            x => x.Add(It.IsAny<Invoice>()));

        var handler = new AddInvoiceCommandHandler(
            _invoiceRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act 
        var invoiceDto = await handler.Handle(command, default);

        // Assert
        _invoiceRepositoryMock.Verify(
            x => x.Add(It.Is<Invoice>(x => x.Id == invoiceDto.Id)),
            Times.Once);
    }
}
