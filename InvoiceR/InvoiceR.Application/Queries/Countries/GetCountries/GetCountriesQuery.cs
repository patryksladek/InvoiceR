using InvoiceR.Application.Dto;
using MediatR;

namespace InvoiceR.Application.Queries.Countries.GetCountries;

public record GetCountriesQuery : IRequest<IReadOnlyCollection<CountryDto>>;