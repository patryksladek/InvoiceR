using FluentValidation.TestHelper;
using InvoiceR.Application.Commands.Invoices.AddInvoice;
using InvoiceR.Domain.Abstractions.Repositories;
using Moq;

namespace InvoiceR.UnitTests.Commands.Invoices.AddInvoice;

public class AddInvoiceCommandValidatorTests
{
    private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock;

    public AddInvoiceCommandValidatorTests()
    {
        _invoiceRepositoryMock = new();
    }

    [Fact]
    public void ValidationResult_Should_Not_HaveAnyValidationErrors_WhenAddInvoiceCommandIsValidated()
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

        var validator = new AddInvoiceCommandValidator();

        // Act
        var validationResult = validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveAnyValidationErrors();
    }
}