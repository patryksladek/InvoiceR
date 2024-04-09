namespace InvoiceR.Domain.Abstractions.DataExporter;

public interface IDataExporter
{
    void ExportData<T>(IList<T> data, string filePath);
    void SetExportStrategy(IExportStrategy exportStrategy);
}
