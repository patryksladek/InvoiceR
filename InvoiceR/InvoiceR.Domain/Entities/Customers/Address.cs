namespace InvoiceR.Domain.Entities.Customers;

public class Address : AuditableEntity
{
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string Building { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }

    public int CountryId { get; set; }
    public Country Country { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
