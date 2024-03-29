using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.InvoiceItems.GetInvoiceItemById;
public record GetInvoiceItemByIdQuery(int Id) : IQuery<InvoiceItemDetailDto>;
