using System.Net;

namespace InvoiceR.Domain.Exceptions;

public class NotEmptyDatabaseException : InvoiceRException
{
    public NotEmptyDatabaseException()
        : base("Unable to generate data on a non-empty database.")
    { }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
