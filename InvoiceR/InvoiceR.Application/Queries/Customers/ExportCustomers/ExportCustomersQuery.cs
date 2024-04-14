using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Customers.ExportCustomers;

public record ExportCustomersQuery(int[] Ids, ExportTypeDto ExportType) : IQuery<FileDto>;
