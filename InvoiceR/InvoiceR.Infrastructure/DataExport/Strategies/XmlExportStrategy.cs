using InvoiceR.Domain.Abstractions.DataExporter;
using System.Xml.Serialization;

namespace InvoicePI.Infrastructure.DataExport.Strategies;

public class XmlExportStrategy : IExportStrategy
{
    public void Export<T>(IList<T> data, string filePath)
    {
        var serializer = new XmlSerializer(typeof(List<T>));
        using (var streamWriter = new StreamWriter(filePath))
        {
            serializer.Serialize(streamWriter, data);
        }
    }
}
