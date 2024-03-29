using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Commands.Invoices.AddInvoice;

public record GetInvoicesQuery() : IQuery<IReadOnlyCollection<InvoiceDto>>;