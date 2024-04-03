using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Invoices.GetInvoices;

public record GetInvoicesQuery() : IQuery<IReadOnlyCollection<InvoiceDto>>;