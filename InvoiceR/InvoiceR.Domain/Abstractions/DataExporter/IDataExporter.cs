namespace InvoiceR.Domain.Abstractions.DataExporter;

public interface IDataExporter
{
    byte[] ExportData<T>(IList<T> data);
    void SetExportStrategy(IExportStrategy exportStrategy);
}
