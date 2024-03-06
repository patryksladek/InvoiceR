namespace InvoiceR.Domain.Exceptions;

public class CustomerNotFoundException : Exception
{
    public int Id { get; set; }
    public CustomerNotFoundException(int id)
        : base($"Customer with id {id} not found.")
        => Id = id;
}
