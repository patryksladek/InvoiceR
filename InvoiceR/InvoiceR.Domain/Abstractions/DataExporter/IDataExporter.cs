using InvoiceR.Domain.Enums;

namespace InvoiceR.Domain.Abstractions.DataExporter;

public interface IDataExporter
{
    byte[] ExportData<T>(IEnumerable<T> data);
    void SetExportStrategy(ExportType exportStrategyType);
}
