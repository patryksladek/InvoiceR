using CsvHelper;
using InvoiceR.Domain.Abstractions.DataExporter;
using System.Globalization;

namespace InvoiceR.Infrastructure.DataExport.Strategies;

public class CsvExportStrategy : IExportStrategy
{
    public byte[] Export<T>(IList<T> data)
    {
        using (var memoryStream = new MemoryStream())
        using (var writer = new StreamWriter(memoryStream))
        using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
        {
            csv.WriteRecords(data);
            writer.Flush();
            return memoryStream.ToArray();
        }
    }
}
