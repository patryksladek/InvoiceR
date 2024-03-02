namespace InvoiceR.Domain.Entities.Customers;

public class Country : BaseEntity
{
    public string Symbol { get; set; }
    public string Name { get; set; }
    public ICollection<Address> Addresses { get; set; }
}