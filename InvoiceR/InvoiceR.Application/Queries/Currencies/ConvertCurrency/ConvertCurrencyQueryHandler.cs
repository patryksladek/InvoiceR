using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Domain.Abstractions;

namespace InvoiceR.Application.Queries.Currencies.ConvertCurrency;

internal class ConvertCurrencyQueryHandler : IQueryHandler<ConvertCurrencyQuery, decimal>
{
    private readonly ICurrencyConverter _currencyConverter;

    public ConvertCurrencyQueryHandler(ICurrencyConverter currencyConverter)
    {
        _currencyConverter = currencyConverter;
    }
    public Task<decimal> Handle(ConvertCurrencyQuery request, CancellationToken cancellationToken)
    {
        decimal value = _currencyConverter.Convert(request.Amount, request.FromCurrencySymbol, request.ToCurrencySymbol, request.Date);
        return Task.FromResult(value);
    }
}
