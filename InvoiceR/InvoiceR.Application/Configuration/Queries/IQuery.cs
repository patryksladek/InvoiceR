using MediatR;

namespace InvoiceR.Application.Configuration.Queries;

public interface IQuery<out TResult> : IRequest<TResult>
{
}
