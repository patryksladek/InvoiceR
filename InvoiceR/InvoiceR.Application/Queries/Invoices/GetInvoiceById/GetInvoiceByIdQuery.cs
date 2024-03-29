using InvoiceR.Application.Dto;
using InvoiceR.Application.Configuration.Queries;
using MediatR;

namespace InvoiceR.Application.Queries.Invoices.GetInvoiceById;
public record GetInvoiceByIdQuery(int Id) : IQuery<InvoiceDetailDto>;
