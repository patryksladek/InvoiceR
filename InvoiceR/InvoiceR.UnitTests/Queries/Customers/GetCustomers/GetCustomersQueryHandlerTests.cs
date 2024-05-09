using Castle.Core.Resource;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Queries.Customers.GetCustomers;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Customers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceR.UnitTests.Queries.Customers.GetCustomers;

public class GetCustomersQueryHandlerTests
{
    private readonly Mock<ICustomerReadOnlyRepository> _customerReadOnlyRepository;

    public GetCustomersQueryHandlerTests()
    {
        _customerReadOnlyRepository = new();
    }

    [Fact]
    public void Handle_Should_CallGetAllAsyncOnRepository_WhenGetCustomersQuery()
    {
        // Arrange
        var customersQuery = new List<Customer>().AsQueryable();

        _customerReadOnlyRepository.Setup(
            x => x.GetAllAsync()).Returns(customersQuery);

        var handler = new GetCustomersQueryHandler(
            _customerReadOnlyRepository.Object);

        // Act
        handler.Handle(new GetCustomersQuery(), default);

        // Assert
        _customerReadOnlyRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}