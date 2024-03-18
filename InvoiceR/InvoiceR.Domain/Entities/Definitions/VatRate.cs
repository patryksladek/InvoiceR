using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Entities.Definitions;

public class VatRate : BaseEntity
{
    public string Symbol { get; set; }
    public decimal Value { get; set; }
    public ICollection<Product> Products { get; set; }
}

