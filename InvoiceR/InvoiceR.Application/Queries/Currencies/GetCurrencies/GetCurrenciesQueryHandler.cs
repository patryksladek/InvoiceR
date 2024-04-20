using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Currencies.GetCurrencies;

internal class GetCurrenciesQueryHandler : IQueryHandler<GetCurrenciesQuery, IReadOnlyCollection<CurrencyDto>>
{
    private readonly ICurrencyReadOnlyRepository _currencyReadOnlyRepository;

    public GetCurrenciesQueryHandler(ICurrencyReadOnlyRepository currencyReadOnlyRepository)
    {
        _currencyReadOnlyRepository = currencyReadOnlyRepository;
    }

    public async Task<IReadOnlyCollection<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _currencyReadOnlyRepository.GetAllAsync().ToListAsync();

        var currenciesDto = currencies.Adapt<IReadOnlyCollection<CurrencyDto>>();

        return currenciesDto;
    }
}
