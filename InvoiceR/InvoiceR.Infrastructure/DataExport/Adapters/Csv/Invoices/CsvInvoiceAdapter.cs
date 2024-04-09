using InvoiceR.Application.Dto;

namespace InvoiceR.Infrastructure.DataExport.Adapters.Csv.Invoices;
public class CsvInvoiceAdapter
{
    public static IList<Invoice> Convert(IList<InvoiceDto> invoiceDtos)
    {
        IList<Invoice> invoice = new List<Invoice>();

        foreach (InvoiceDto invoiceDto in invoiceDtos)
            foreach (InvoiceItemDto invoiceItemDto in invoiceDto.InvoiceItems)
                invoice.Add(new Invoice(invoiceDto, invoiceItemDto));
        
        return invoice;
    }
}
