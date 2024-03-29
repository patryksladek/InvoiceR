namespace InvoiceR.Domain.Exceptions;

public class InvoiceNotFoundException : Exception
{
    public int Id { get; set; }
    public InvoiceNotFoundException(int id)
        : base($"Invoice with id {id} not found.")
        => Id = id;
}
