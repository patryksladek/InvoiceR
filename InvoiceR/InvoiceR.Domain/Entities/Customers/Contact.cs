namespace InvoiceR.Domain.Entities.Customers;

public class Contact : AuditableEntity
{
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}