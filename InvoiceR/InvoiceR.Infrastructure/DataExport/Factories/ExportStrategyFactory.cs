using InvoicePI.Infrastructure.DataExport.Strategies;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Enums;
using InvoiceR.Infrastructure.DataExport.Strategies;

namespace InvoiceR.Infrastructure.DataExport.Factories;

public class ExportStrategyFactory : IExportStrategyFactory
{
    public IExportStrategy CreateExportStrategy(ExportType exportType)
    {
        switch (exportType)
        {
            case ExportType.Csv:
                return new CsvExportStrategy();
            case ExportType.Xls:
                return new XlsExportStrategy();
            case ExportType.Xml:
                return new XmlExportStrategy();
            default:
                throw new ArgumentException("Unsupported export type.", nameof(exportType));
        }
    }
}
