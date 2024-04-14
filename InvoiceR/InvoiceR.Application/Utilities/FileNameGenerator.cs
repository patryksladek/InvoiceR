using InvoiceR.Domain.Enums;

namespace InvoiceR.Application.Utilities;

internal static class FileNameGenerator
{
    public static string GenerateFileName(ExportType exportType, ExportObject exportObject)
    {
        string fileExtension = exportType switch
        {
            ExportType.Csv => ".csv",
            ExportType.Xls => ".xls",
            ExportType.Xml => ".xml",
            _ => throw new ArgumentOutOfRangeException(nameof(exportType), exportType, "Unsupported export type.")
        };

        return $"exported_{exportObject.ToString().ToLowerInvariant()}{fileExtension}";
    }
}