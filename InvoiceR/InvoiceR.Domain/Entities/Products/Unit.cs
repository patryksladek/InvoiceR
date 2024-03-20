namespace InvoiceR.Domain.Entities.Products;

public class Unit : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; }
}