using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
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
        var currencies = _currencyReadOnlyRepository.GetAllAsync();

        var currenciesDto = await currencies.Select(x => new CurrencyDto()
        {
            Id = x.Id,
            Symbol = x.Symbol
        })
        .ToListAsync();

        return currenciesDto;
    }
}
