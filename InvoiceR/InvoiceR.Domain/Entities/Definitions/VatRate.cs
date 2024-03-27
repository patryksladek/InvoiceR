using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Entities.Definitions;

public class VatRate : BaseEntity
{
    public string Symbol { get; set; }
    public decimal Value { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
}

