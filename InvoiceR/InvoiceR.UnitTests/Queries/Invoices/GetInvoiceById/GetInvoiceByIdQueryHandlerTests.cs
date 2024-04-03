using FluentAssertions;
using InvoiceR.Application.Queries.Invoices.GetInvoiceById;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using Moq;

namespace InvoiceR.UnitTests.Queries.Invoices.GetInvoiceById;

public class GetInvoiceByIdQueryHandlerTests
{
    private readonly Mock<IInvoiceReadOnlyRepository> _invoiceReadOnlyRepository;

    public GetInvoiceByIdQueryHandlerTests()
    {
        _invoiceReadOnlyRepository = new();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository_WhenGetInvoiceByIdQuery()
    {
        // Arrange
        var invoice = new Invoice()
        {
            Id = 1,
            Number = "FV/20240403/1",
            Date = DateOnly.Parse("2024-04-03"),
            CustomerId = 1,
            Customer = new Customer()
            {
                Id = 1,
                Name = "Customer 1",
                NIP = "1234567890",
                Address = new Address
                {
                    Street = "Street 1",
                    StreetNumber = "123",
                    Building = "A",
                    City = "City 1",
                    Country = new Country { Id = 1, Name = "Country 1" }
                },
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "customer1@example.com"
                }
            },
            Description = "Description",
            Net = 1000,
            Vat = 230,
            Gross = 1230,
            CurrencyId = 1,
            Currency = new Currency()
            {
                Id = 1,
                Name = "Polish zloty",
                Symbol = "PLN",
                IsDefault = true
            },
            Status = InvoiceStatus.Buffer,
            InvoiceItems = new List<InvoiceItem>()
            {
                new InvoiceItem()
                {
                    InvoiceId = 1,
                    OrdinalNumber = 1,
                    Product = new Product()
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
                        NetPrice = 1000
                    },
                    Quantity = 1,
                    Price = 1000,
                    Net = 1000,
                    Gross = 1230,
                    CurrencyId = 1,
                    Currency = new Currency()
                    {
                        Id = 1,
                        Name = "Polish zloty",
                        Symbol = "PLN",
                        IsDefault = true
                    },
                    VatRateId = 6,
                    VatRate = new VatRate()
                    {
                        Id = 6,
                        Symbol = "23%",
                        Value = 0.23m
                    }
                }
            }
        };

        _invoiceReadOnlyRepository.Setup(
            x => x.GetByIdWithDetailAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoice);

        var handler = new GetInvoiceByIdQueryHandler(
            _invoiceReadOnlyRepository.Object);

        // Act
        await handler.Handle(new GetInvoiceByIdQuery(1), default);

        // Assert
        _invoiceReadOnlyRepository.Verify(
           x => x.GetByIdWithDetailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),
           Times.Once
           );
    }

    [Fact]
    public async Task Handle_Should_ReturnInvoice_WhenGetInvoiceByIdQuery()
    {
        // Arrange
        var invoice = new Invoice()
        {
            Id = 1,
            Number = "FV/20240403/1",
            Date = DateOnly.Parse("2024-04-03"),
            CustomerId = 1,
            Customer = new Customer()
            {
                Id = 1,
                Name = "Customer 1",
                NIP = "1234567890",
                Address = new Address
                {
                    Street = "Street 1",
                    StreetNumber = "123",
                    Building = "A",
                    City = "City 1",
                    Country = new Country { Id = 1, Name = "Country 1" }
                },
                Contact = new Contact
                {
                    Phone = "123456789",
                    Email = "customer1@example.com"
                }
            },
            Description = "Description",
            Net = 1000,
            Vat = 230,
            Gross = 1230,
            CurrencyId = 1,
            Currency = new Currency()
            {
                Id = 1,
                Name = "Polish zloty",
                Symbol = "PLN",
                IsDefault = true
            },
            Status = InvoiceStatus.Buffer,
            InvoiceItems = new List<InvoiceItem>()
            {
                new InvoiceItem()
                {
                    InvoiceId = 1,
                    OrdinalNumber = 1,
                    Product = new Product()
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
                        NetPrice = 1000
                    },
                    Quantity = 1,
                    Price = 1000,
                    Net = 1000,
                    Gross = 1230,
                    CurrencyId = 1,
                    Currency = new Currency()
                    {
                        Id = 1,
                        Name = "Polish zloty",
                        Symbol = "PLN",
                        IsDefault = true
                    },
                    VatRateId = 6,
                    VatRate = new VatRate()
                    {
                        Id = 6,
                        Symbol = "23%",
                        Value = 0.23m
                    }
                }
            }
        };

        _invoiceReadOnlyRepository.Setup(
            x => x.GetByIdWithDetailAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoice);

        var handler = new GetInvoiceByIdQueryHandler(
           _invoiceReadOnlyRepository.Object);

        // Act
        var invoiceDto = await handler.Handle(new GetInvoiceByIdQuery(1), default);

        // Assert
        invoiceDto.Should().NotBeNull();
        invoiceDto.Id.Should().Be(invoice.Id);
        invoiceDto.Number.Should().Be(invoice.Number);
        invoiceDto.Date.Should().Be(invoice.Date);
        invoiceDto.Description.Should().Be(invoice.Description);
        invoiceDto.Net.Should().Be(invoice.Net);
        invoiceDto.Vat.Should().Be(invoice.Vat);
        invoiceDto.Gross.Should().Be(invoice.Gross);
        invoiceDto.IsApproved.Should().Be(invoice.Status == InvoiceStatus.Confirmed ? true : false);
        invoiceDto.CurrencyId.Should().Be(invoice.CurrencyId);
        invoiceDto.CustomerId.Should().Be(invoice.CustomerId);
        invoiceDto.InvoiceItems.Count().Should().Be(invoice.InvoiceItems.Count());
    }
}