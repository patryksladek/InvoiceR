using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using MediatR;

namespace InvoiceR.Application.Queries.Customers.GetCustomerById;

public record GetCustomerByIdQuery(int Id) : IQuery<CustomerDetailDto>;