using InvoiceR.Domain.Abstractions.DataExporter;
using OfficeOpenXml;

namespace InvoiceR.Infrastructure.DataExport.Strategies;

public class XlsExportStrategy : IExportStrategy
{
    public byte[] Export<T>(IEnumerable<T> data)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var memoryStream = new MemoryStream())
        {
            using (var package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Records");
                worksheet.Cells.LoadFromCollection(data, true);
            }
            return memoryStream.ToArray();
        }
    }
}
