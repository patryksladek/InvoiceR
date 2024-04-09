using InvoiceR.Application.Dto;

namespace InvoiceR.Infrastructure.DataExport.Adapters.Xml.Invoices;

public class XmlInvoiceAdapter
{
    public static IList<Invoice> Convert(IList<InvoiceDto> invoiceDtos)
        =>  invoiceDtos.Select(dto => (Invoice)dto).ToList();
}
