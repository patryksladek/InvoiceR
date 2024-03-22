using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Currencies.GetCurrencies;

public record GetCurrenciesQuery : IQuery<IReadOnlyCollection<CurrencyDto>>;
