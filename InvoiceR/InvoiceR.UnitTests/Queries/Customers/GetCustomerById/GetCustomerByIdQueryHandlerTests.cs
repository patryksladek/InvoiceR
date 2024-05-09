using Castle.Core.Resource;
using FluentAssertions;
using InvoiceR.Application.Queries.Customers.GetCustomerById;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Customers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceR.UnitTests.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQueryHandlerTests
{
    private readonly Mock<ICustomerReadOnlyRepository> _customerReadOnlyRepository;

    public GetCustomerByIdQueryHandlerTests()
    {
        _customerReadOnlyRepository = new();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository_WhenGetCustomersByIdQuery()
    {
        // Arrange
        var customer = new Customer()
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
        };

        _customerReadOnlyRepository.Setup(
            x => x.GetByIdWithDetailAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var handler = new GetCustomerByIdQueryHandler(
            _customerReadOnlyRepository.Object);

        // Act
        await handler.Handle(new GetCustomerByIdQuery(1), default);

        // Assert
        _customerReadOnlyRepository.Verify(
           x => x.GetByIdWithDetailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),
           Times.Once
           );
    }

    [Fact]
    public async Task Handle_Should_ReturnCustomer_WhenGetCustomerByIdQuery()
    {
        // Arrange
        var customer = new Customer()
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
        };

         _customerReadOnlyRepository.Setup(
            x => x.GetByIdWithDetailAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var handler = new GetCustomerByIdQueryHandler(
           _customerReadOnlyRepository.Object);

        // Act
        var customerDto = await handler.Handle(new GetCustomerByIdQuery(1), default);

        // Assert
        customerDto.Should().NotBeNull();
        customerDto.Id.Should().Be(customer.Id);
        customerDto.Name.Should().Be(customer.Name);
        customerDto.NIP.Should().Be(customer.NIP);
        customerDto.Email.Should().Be(customer.Contact.Email);
        customerDto.Phone.Should().Be(customer.Contact.Phone);
        customerDto.Street.Should().Be(customer.Address.Street);
        customerDto.StreetNumber.Should().Be(customer.Address.StreetNumber);
        customerDto.Building.Should().Be(customer.Address.Building);
        customerDto.City.Should().Be(customer.Address.City);
        customerDto.CountryId.Should().Be(customer.Address.Country.Id);
    }
}