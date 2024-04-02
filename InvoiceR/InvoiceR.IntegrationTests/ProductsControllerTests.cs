using FluentAssertions;
using FluentAssertions.Equivalency;
using InvoiceR.Application.Commands.Products.AddProduct;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Entities.Definitions;
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

public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public ProductsControllerTests(WebApplicationFactory<Program> factory)
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
    public async Task GetAll_Should_ReturnListOfProductsAndStatusCodeOK()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory?.CreateScope();
        var _dbContext = scope?.ServiceProvider.GetService<InvoicerDbContext>();


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

        _dbContext?.Products.AddRange(
            new Product()
            {
                Id = 1,
                Name = "Product 1",
                Type = ProductType.Product,
                Barcode = "978020137962",
                BarcodeType = ProductBarcodeType.EAN13,
                CurrencyId = 1,
                UnitId = 1,
                VatRateId = 6,
                NetPrice = 100
            },
            new Product()
            {
                Id = 2,
                Name = "Service 2",
                Type = ProductType.Service,
                Barcode = "978020137964",
                BarcodeType = ProductBarcodeType.EAN13,
                CurrencyId = 1,
                UnitId = 1,
                VatRateId = 7,
                NetPrice = 200
            });

        await _dbContext?.SaveChangesAsync();

        // Act
        var response = await _httpClient.GetAsync("/api/products");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Post_Should_ReturnNewProductAndStatusCodeCreated()
    {
        // Arrange
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            ProductType = (int)ProductType.Product,
            Barcode = "978020137962",
            ProductBarcodeType = (int)ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            Price = 100
        };

        var jsonString = JsonConvert.SerializeObject(command);
        var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/products", stringContent);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ProductDto>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Should().NotBeNull();
        result.Should().BeOfType<ProductDto>();

        Assert.Equal(result.Name, command.Name);
        Assert.Equal(result.Barcode, command.Barcode);
        Assert.Equal(result.Price, command.Price);
    }

    [Fact]
    public async Task Delete_Should_ReturnStatusCodeNoContent()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<InvoicerDbContext>();

        var product = _dbContext.Products.Add(
            new Product()
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
            });

        await _dbContext.SaveChangesAsync();

        int productId = product.Entity.Id;

        // Act
        var response = await _httpClient.DeleteAsync($"/api/products/{productId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}