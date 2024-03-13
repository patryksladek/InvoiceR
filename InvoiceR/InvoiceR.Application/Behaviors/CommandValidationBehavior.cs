using FluentValidation.Results;
using FluentValidation;
using MediatR;

namespace InvoiceR.Application.Behaviors;

public class CommandValidationBehavior<TRequst, TResponse> : IPipelineBehavior<TRequst, TResponse>
    where TRequst : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequst>> _validators;

    public CommandValidationBehavior(IEnumerable<IValidator<TRequst>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequst request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequst>(request);
        var errorList = _validators
            .Select(async x => await x.ValidateAsync(context))
            .Select(x => x.Result)
            .SelectMany(x => x.Errors)
            .Where(x => x != null && x.Severity == Severity.Error)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessage) => new ValidationFailure()
                {
                    PropertyName = propertyName,
                    ErrorMessage = string.Join(",", errorMessage)
                })
            .ToList();

        if (errorList.Any())
        {
            throw new ValidationException("Invalid command, reasons:", errorList);
        }

        return await next();
    }
}