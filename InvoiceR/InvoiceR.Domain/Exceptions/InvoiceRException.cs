using System.Net;

namespace InvoiceR.Domain.Exceptions;

public abstract class InvoiceRException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    public InvoiceRException(string messege) : base(messege)
    {

    }
}

