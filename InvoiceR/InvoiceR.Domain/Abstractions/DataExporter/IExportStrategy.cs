namespace InvoiceR.Domain.Abstractions.DataExporter;

public interface IExportStrategy
{
    byte[] Export<T>(IList<T> data);
}
