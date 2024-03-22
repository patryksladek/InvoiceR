using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Units.GetUnits;

public record GetUnitsQuery : IQuery<IReadOnlyCollection<UnitDto>>;