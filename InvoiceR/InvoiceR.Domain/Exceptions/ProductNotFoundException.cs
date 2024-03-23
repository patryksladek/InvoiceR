namespace InvoiceR.Domain.Exceptions;

public class ProductNotFoundException : Exception
{
    public int Id { get; set; }
    public ProductNotFoundException(int id)
        : base($"Product with id {id} not found.")
        => Id = id;
}
