namespace InvoiceR.Domain.Abstractions.DataExporter;

public interface IExportStrategy
{
    void Export<T>(IList<T> data, string filePath);
}
