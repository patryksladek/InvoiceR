using InvoiceR.Application.Queries.Invoices.GetInvoices;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Invoices;
using Moq;

namespace InvoiceR.UnitTests.Queries.Invoices.GetInvoices;

public class GetInvoicesQueryHandlerTests
{
    private readonly Mock<IInvoiceReadOnlyRepository> _invoiceReadOnlyRepository;

    public GetInvoicesQueryHandlerTests()
    {
        _invoiceReadOnlyRepository = new();
    }

    [Fact]
    public void Handle_Should_CallGetAllAsyncOnRepository_WhenGetInvoicesQuery()
    {
        // Arrange
        var invoicesQuery = new List<Invoice>().AsQueryable();

        _invoiceReadOnlyRepository.Setup(
            x => x.GetAllAsync()).Returns(invoicesQuery);

        var handler = new GetInvoicesQueryHandler(
            _invoiceReadOnlyRepository.Object);

        // Act
        handler.Handle(new GetInvoicesQuery(), default);

        // Assert
        _invoiceReadOnlyRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}