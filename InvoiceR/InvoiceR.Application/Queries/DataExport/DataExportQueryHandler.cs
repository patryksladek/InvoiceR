using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Queries.DataExport;

internal class DataExportQueryHandler : IQueryHandler<DataExportQuery, byte[]>
{
    private readonly IDataExporter _dataExporter;

    public DataExportQueryHandler(IDataExporter dataExporter)
    {
        _dataExporter = dataExporter;
    }

    public async Task<byte[]> Handle(DataExportQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}
