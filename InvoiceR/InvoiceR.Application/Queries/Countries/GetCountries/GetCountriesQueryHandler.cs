using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions.Repositories;
using Mapster;
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
        var countries = await _countryReadOnlyRepository.GetAllAsync().ToListAsync();

        var countriesDto = countries.Adapt<IReadOnlyCollection<CountryDto>>();

        return countriesDto;
    }
}
