using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Queries.DataExport;

internal class DataExportQueryHandler : IQueryHandler<DataExportQuery, byte[]>
{
    private readonly IExportStrategy _exportStrategy;

    public DataExportQueryHandler(IExportStrategy dataGenerator)
    {
        _exportStrategy = dataGenerator;
    }

    public async Task<byte[]> Handle(DataExportQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}
