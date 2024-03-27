using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Entities.Definitions;

public class Currency : BaseEntity
{
    public string Symbol { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public ICollection<ExchangeRate> ExchangeRates { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
}