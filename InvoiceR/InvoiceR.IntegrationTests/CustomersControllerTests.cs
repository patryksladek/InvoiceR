using FluentAssertions;
using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Enums;
using InvoiceR.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceR.IntegrationTests;

public class CustomersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public CustomersControllerTests(WebApplicationFactory<Program> factory)
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
    public async Task GetAll_Should_ReturnListOfCustomersAndStatusCoseOK()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<InvoicerDbContext>();

        _dbContext.Customers.AddRange(
            new Customer()
            {
                Id = 1,
                Name = "Customer 1",
                NIP = "1234567890",
                Segment = CustomerSegment.HomeOffice,
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
                },
                IsActive = true
            },
            new Customer()
            {
                Id = 2,
                Name = "Customer 2",
                NIP = "0987654321",
                Segment = CustomerSegment.SmallBusiness,
                Address = new Address
                {
                    Street = "Street 2",
                    StreetNumber = "321",
                    Building = "B",
                    City = "City 2",
                    Country = new Country { Id = 2, Symbol = "C2", Name = "Country 2" }
                },
                Contact = new Contact
                {
                    Phone = "987654321",
                    Email = "customer2@example.com"
                },
                IsActive = true
            });

        await _dbContext.SaveChangesAsync();

        // Act
        var response = await _httpClient.GetAsync("/api/customers");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Post_Should_ReturnNewCustomerAndStatusCodeCreated()
    {
        // Arrange
        var command = new AddCustomerCommand()
        {
            Name = "Customer 1",
            NIP = "1234567890",
            Segment = 1,
            Street = "Street 1",
            StreetNumber = "123",
            Building = "A",
            City = "City 1",
            PostalCode = "12-345",
            CountryId = 1,
            Phone = "123456789",
            Email = "customer1@example.com",
            Site = "customer1.com",
            IsActive = true
        };

        var jsonString = JsonConvert.SerializeObject(command);
        var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/customers", stringContent);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CustomerDto>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Should().NotBeNull();
        result.Should().BeOfType<CustomerDto>();

        Assert.Equal(result.Name, command.Name);
        Assert.Equal(result.NIP, command.NIP);
        Assert.Equal(result.Email, command.Email);
    }

    [Fact]
    public async Task Delete_Should_ReturnStatusCodeNoContent()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<InvoicerDbContext>();

        var student = _dbContext.Customers.Add(
            new Customer()
            {
                Id = 1,
                Name = "Customer 1",
                NIP = "1234567890",
                Segment = CustomerSegment.HomeOffice,
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
                },
                IsActive = true
            });

        await _dbContext.SaveChangesAsync();

        int studentId = student.Entity.Id;

        // Act
        var response = await _httpClient.DeleteAsync($"/api/customers/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}