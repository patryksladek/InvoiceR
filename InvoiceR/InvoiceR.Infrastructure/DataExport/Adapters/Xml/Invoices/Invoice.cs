using System.Xml.Serialization;
using InvoiceR.Application.Dto;

namespace InvoiceR.Infrastructure.DataExport.Adapters.Xml.Invoices;

public class Invoice
{
    public bool IsApproved { get; set; }
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public string Customer { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public string Currency { get; set; }

    [XmlArray("InvoiceItems")]
    [XmlArrayItem("InvoiceItem")]
    public List<InvoiceItem> InvoiceItems { get; set; }

    public Invoice() { }

    public Invoice(InvoiceDto original)
    {
        IsApproved = original.IsApproved;
        Number = original.Number;
        Date = original.Date;
        Customer = original.Customer;
        Net = original.Net;
        Vat = original.Vat;
        Gross = original.Gross;
        Currency = original.Currency;

        InvoiceItems = original.InvoiceItems.Select(item => (InvoiceItem)item).ToList();
    }

    public static explicit operator Invoice(InvoiceDto original)
    {
        return new Invoice(original);
    }

    public static explicit operator InvoiceDto(Invoice xml)
    {
        return new InvoiceDto
        {
            IsApproved = xml.IsApproved,
            Number = xml.Number,
            Date = xml.Date,
            Customer = xml.Customer,
            Net = xml.Net,
            Vat = xml.Vat,
            Gross = xml.Gross,
            Currency = xml.Currency,
            InvoiceItems = xml.InvoiceItems.Select(item => (InvoiceItemDto)item).ToList()
        };
    }

    public class InvoiceItem
    {
        public int OrdinalNumber { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public string Currency { get; set; }

        public static explicit operator InvoiceItem(InvoiceItemDto original)
        {
            return new InvoiceItem
            {
                OrdinalNumber = original.OrdinalNumber,
                Product = original.Product,
                Quantity = original.Quantity,
                Price = original.Price,
                Net = original.Net,
                Gross = original.Gross,
                Currency = original.Currency
            };
        }

        public static explicit operator InvoiceItemDto(InvoiceItem xml)
        {
            return new InvoiceItemDto
            {
                OrdinalNumber = xml.OrdinalNumber,
                Product = xml.Product,
                Quantity = xml.Quantity,
                Price = xml.Price,
                Net = xml.Net,
                Gross = xml.Gross,
                Currency = xml.Currency
            };
        }
    }

}