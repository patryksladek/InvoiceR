using InvoiceR.Domain.Abstractions.DataExporter;

namespace InvoiceR.Infrastructure.DataExport;

public class DataExporter : IDataExporter
{
    private IExportStrategy _exportStrategy;

    public DataExporter(IExportStrategy exportStrategy)
    {
        _exportStrategy = exportStrategy;
    }

    public void SetExportStrategy(IExportStrategy exportStrategy)
    {
        _exportStrategy = exportStrategy;
    }

    public void ExportData<T>(IList<T> data, string filePath)
    {
        _exportStrategy.Export(data, filePath);
    }
}
