using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Countries.GetCountries;

internal class GetCountriesQueryHandler : IQueryHandler<GetCountriesQuery, IReadOnlyCollection<CountryDto>>
{
    private readonly ICountryReadOnlyRepository _countryReadOnlyRepository;

    public GetCountriesQueryHandler(ICountryReadOnlyRepository countryReadOnlyRepository)
    {
        _countryReadOnlyRepository = countryReadOnlyRepository;
    }

    public async Task<IReadOnlyCollection<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = _countryReadOnlyRepository.GetAllAsync();

        var countriesDto = await countries.Select(x => new CountryDto()
        {
            Id = x.Id,
            Symbol = x.Symbol,
            Name = x.Name
        })
        .ToListAsync();

        return countriesDto;
    }
}
