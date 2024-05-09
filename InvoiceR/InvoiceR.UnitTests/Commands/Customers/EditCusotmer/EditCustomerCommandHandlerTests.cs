using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Exceptions;
using Moq;

namespace InvoiceR.UnitTests.Commands.Customers.EditCusotmer;


public class EditCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public EditCustomerCommandHandlerTests()
    {
        _customerRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallUpdateOnReposiotry_WhenNameIsUnique()
    {
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

        _customerRepositoryMock.Setup(
            x => x.Update(It.IsAny<Customer>()));

        _customerRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act 
        var customerDto = await handler.Handle(command, default);

        // Assert
        _customerRepositoryMock.Verify(
            x => x.Add(It.Is<Customer>(x => x.Id == customerDto.Id)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrowCustomerAlreadyExistsException_WhenNameIsNotUnique()
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

        _customerRepositoryMock.Setup(
             x => x.Update(It.IsAny<Customer>()));

        _customerRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new AddCustomerCommandHandler(
           _customerRepositoryMock.Object,
           _unitOfWorkMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<CustomerWithSameNameAlreadyExistsException>(async () => await handler.Handle(command, default));
    }
}
