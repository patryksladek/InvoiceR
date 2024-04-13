using InvoiceR.Domain.Enums;

namespace InvoiceR.Domain.Abstractions.DataExporter;

public interface IExportStrategyFactory
{
    IExportStrategy CreateExportStrategy(ExportType exportType);
}
