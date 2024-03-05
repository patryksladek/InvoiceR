using InvoiceR.Application.Dto;
using MediatR;

namespace InvoiceR.Application.Queries.Customers.GetCustomers;

public record GetCustomersQuery() : IRequest<IReadOnlyCollection<CustomerDto>>;