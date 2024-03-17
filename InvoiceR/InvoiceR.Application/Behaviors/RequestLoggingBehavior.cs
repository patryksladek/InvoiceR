using FluentValidation.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InvoiceR.Application.Behaviors;

internal class RequestLoggingBehavior
{
}

public class RequestLoggingBehavior<TRequst, TResponse>(ILogger<RequestLoggingBehavior<TRequst, TResponse>> logger) : IPipelineBehavior<TRequst, TResponse>
    where TRequst : class
{
    public async Task<TResponse> Handle(TRequst request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequst).Name;

        logger.LogInformation($"Processing request {requestName}");

        return await next();
    }
}
