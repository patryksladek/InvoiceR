using System.Net;

namespace InvoiceR.Domain.Exceptions;

public class CustomerWithSameNameAlreadyExistsException : InvoiceRException
{
    public string Name { get; set; }

    public CustomerWithSameNameAlreadyExistsException(string name) 
        : base($"Customer with name {name} already exists.")
        => Name = name;

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
