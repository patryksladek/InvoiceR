using InvoiceR.Domain.Abstractions.DataExporter;

namespace InvoiceR.Infrastructure.DataExport;

public class DataExporter : IDataExporter
{
    private IExportStrategy _exportStrategy;

    public void SetExportStrategy(IExportStrategy exportStrategy)
    {
        _exportStrategy = exportStrategy;
    }

    public byte[] ExportData<T>(IList<T> data)
    {
        return _exportStrategy.Export(data);
    }
}
