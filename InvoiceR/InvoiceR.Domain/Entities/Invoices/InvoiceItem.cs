using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Entities.Invoices;

public class InvoiceItem : AuditableEntity
{
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public int OrdinalNumber { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Net { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public int VatRateId { get; set; }
    public VatRate VatRate { get; set; }
}