using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Enums;

namespace InvoiceR.Infrastructure.DataExport;

public class DataExporter : IDataExporter
{
    private readonly IExportStrategyFactory _exportStrategyFactory;
    private IExportStrategy _exportStrategy;

    public DataExporter(IExportStrategyFactory exportStrategyFactory)
    {
        _exportStrategyFactory = exportStrategyFactory;
    }

    public void SetExportStrategy(ExportType exportType)
    {
        _exportStrategy = _exportStrategyFactory.CreateExportStrategy(exportType);
    }

    public byte[] ExportData<T>(IList<T> data)
    {
        return _exportStrategy.Export(data);
    }
}
