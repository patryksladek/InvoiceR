using System.Net;

namespace InvoiceR.Domain.Exceptions;

public class CustomerNotFoundException : InvoiceRException
{
    public int Id { get; set; }
    public CustomerNotFoundException(int id)
        : base($"Customer with id {id} not found.")
        => Id = id;

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
