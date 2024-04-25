using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Customers.GetCustomerByNip;

public record GetCustomerByNipQuery(string nip) : IQuery<CustomerDetailDto>;