using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using MediatR;

namespace InvoiceR.Application.Queries.Customers.GetCustomers;

public record GetCustomersQuery() : IQuery<IReadOnlyCollection<CustomerDto>>;