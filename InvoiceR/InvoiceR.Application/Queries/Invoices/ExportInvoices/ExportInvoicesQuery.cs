using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Customers.ExportCustomers;

public record ExportInvoicesQuery(int[] Ids, ExportTypeDto ExportType) : IQuery<FileDto>;
