using InvoiceR.Domain.Abstractions.DataExporter;
using System.Xml.Serialization;

namespace InvoicePI.Infrastructure.DataExport.Strategies;

public class XmlExportStrategy : IExportStrategy
{
    public byte[] Export<T>(IList<T> data)
    {
        using (var memoryStream = new MemoryStream())
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(memoryStream, data);
            return memoryStream.ToArray();
        }
    }
}
