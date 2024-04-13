using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.DataExport;

public record ExportCustomersQuery(IList<CustomerDto> Data, ExportTypeDto ExportType) : IQuery<FileDto>;
