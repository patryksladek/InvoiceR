using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Countries.GetCountries;

public record GetCountriesQuery : IQuery<IReadOnlyCollection<CountryDto>>;