using InvoiceR.Application.Dto;

namespace InvoiceR.Infrastructure.DataExport.Adapters.Csv.Invoices;
public class Invoice
{
    public bool IsApproved { get; set; }
    public string Number { get; set; }
    public string Date { get; set; }
    public string Customer { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public string Currency { get; set; }
    public string ItemsCount { get; set; }
    public string ItemProduct { get; set; }
    public int ItemQuantity { get; set; }
    public decimal ItemPrice { get; set; }
    public decimal ItemNet { get; set; }
    public decimal ItemGross { get; set; }
    public string ItemCurrency { get; set; }

    public Invoice(InvoiceDto invoiceDto, InvoiceItemDto invoiceItemDto)
    {
        Number = invoiceDto.Number;
        Date = invoiceDto.Date.ToString("dd.MM.yyyy");
        Customer = invoiceDto.Customer;
        Net = invoiceDto.Net;
        Vat = invoiceDto.Vat;
        Gross = invoiceDto.Gross;
        Currency = invoiceDto.Currency;
        IsApproved = invoiceDto.IsApproved;

        ItemProduct = invoiceItemDto.Product;
        ItemQuantity = invoiceItemDto.Quantity;
        ItemPrice = invoiceItemDto.Price;
        ItemNet = invoiceItemDto.Net;
        ItemGross = invoiceItemDto.Gross;
        ItemCurrency = invoiceItemDto.Currency;
    }
}
