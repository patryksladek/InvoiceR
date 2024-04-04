using FluentAssertions;
using FluentAssertions.Equivalency;
using InvoiceR.Application.Commands.Invoices.AddInvoice;
using InvoiceR.Application.Commands.Products.AddProduct;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using InvoiceR.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace InvoiceR.IntegrationTests;

public class InvoicesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public InvoicesControllerTests(WebApplicationFactory<Program> factory)
    {
        _webApplicationFactory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services
                    .SingleOrDefault(services => services.ServiceType == typeof(DbContextOptions<InvoicerDbContext>));
                    services.Remove(dbContextOptions);
                    services.AddDbContext<InvoicerDbContext>(options => options.UseInMemoryDatabase("GradebookDb"));
                });
            });
        _httpClient = _webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Should_ReturnListOfInvoicesAndStatusCodeOK()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory?.CreateScope();
        var _dbContext = scope?.ServiceProvider.GetService<InvoicerDbContext>();

        _dbContext?.Customers.AddRange(new Customer()
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
                Country = new Country { Id = 1, Symbol = "C1", Name = "Country 1" }
            },
            Contact = new Contact
            {
                Phone = "123456789",
                Email = "customer1@example.com"
            }
        });

        _dbContext?.Currencies.AddRange(new Currency()
        {
            Id = 1,
            Name = "Polish zloty",
            Symbol = "PLN",
            IsDefault = true
        });

        _dbContext?.Units.AddRange(new Unit()
        {
            Id = 1,
            Code = "pc",
            Description = "piece - the basic unit of quantity"
        });

        _dbContext?.VatRates.AddRange(new VatRate()
        {
            Id = 6,
            Symbol = "23%",
            Value = 0.23m
        },
        new VatRate()
        {
            Id = 7,
            Symbol = "8%",
            Value = 0.08m
        });

        _dbContext?.Products.AddRange(new Product()
        {
            Id = 1,
            Name = "Product 1",
            Type = ProductType.Product,
            Barcode = "978020137962",
            BarcodeType = ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            NetPrice = 1000
        });

        _dbContext?.Invoices.AddRange(new Invoice()
        {
            Id = 1,
            Number = "FV/20240403/1",
            Date = DateOnly.Parse("2024-04-03"),
            CustomerId = 1,
            Description = "Description",
            Net = 1000,
            Vat = 230,
            Gross = 1230,
            CurrencyId = 1,
            Status = InvoiceStatus.Buffer,
            InvoiceItems = new List<InvoiceItem>()
            {
                new InvoiceItem()
                {
                    InvoiceId = 1,
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
        });
            
        await _dbContext?.SaveChangesAsync();

        // Act
        var response = await _httpClient.GetAsync("/api/Invoices");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<InvoiceDto>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Post_Should_ReturnNewInvoiceAndStatusCodeCreated()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory?.CreateScope();
        var _dbContext = scope?.ServiceProvider.GetService<InvoicerDbContext>();

        _dbContext?.Customers.AddRange(new Customer()
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
                Country = new Country { Id = 1, Symbol = "C1", Name = "Country 1" }
            },
            Contact = new Contact
            {
                Phone = "123456789",
                Email = "customer1@example.com"
            }
        });

        _dbContext?.Currencies.AddRange(new Currency()
        {
            Id = 1,
            Name = "Polish zloty",
            Symbol = "PLN",
            IsDefault = true
        });

        _dbContext?.Units.AddRange(new Unit()
        {
            Id = 1,
            Code = "pc",
            Description = "piece - the basic unit of quantity"
        });

        _dbContext?.VatRates.AddRange(new VatRate()
        {
            Id = 6,
            Symbol = "23%",
            Value = 0.23m
        },
        new VatRate()
        {
            Id = 7,
            Symbol = "8%",
            Value = 0.08m
        });

        _dbContext?.Products.AddRange(new Product()
        {
            Id = 1,
            Name = "Product 1",
            Type = ProductType.Product,
            Barcode = "978020137962",
            BarcodeType = ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            NetPrice = 1000
        });

        var command = new AddInvoiceCommand()
        {
            Number = "FV/20240403/1",
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

        var jsonString = JsonConvert.SerializeObject(command);
        var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/invoices", stringContent);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<InvoiceDto>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Should().NotBeNull();
        result.Should().BeOfType<InvoiceDto>();

        Assert.Equal(result.Number, command.Number);
        Assert.Equal(result.Date, command.Date);
        Assert.Equal(result.Net, command.Net);
        Assert.Equal(result.Vat, command.Vat);
        Assert.Equal(result.Gross, command.Gross);
    }
}