using MediatR;

namespace InvoiceR.Application.Configuration.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}
