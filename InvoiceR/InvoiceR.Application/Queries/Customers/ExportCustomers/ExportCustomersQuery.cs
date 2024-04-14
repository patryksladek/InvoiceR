using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.DataExport;

public record ExportCustomersQuery(int[] Ids, ExportTypeDto ExportType) : IQuery<FileDto>;
