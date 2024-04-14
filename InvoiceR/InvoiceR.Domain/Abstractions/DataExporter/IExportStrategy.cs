namespace InvoiceR.Domain.Abstractions.DataExporter;

public interface IExportStrategy
{
    byte[] Export<T>(IEnumerable<T> data);
}
