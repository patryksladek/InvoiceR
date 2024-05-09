using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Units.GetUnits;

internal class GetUnitsQueryHandler : IQueryHandler<GetUnitsQuery, IReadOnlyCollection<UnitDto>>
{
    private readonly IUnitReadOnlyRepository _unitReadOnlyRepository;

    public GetUnitsQueryHandler(IUnitReadOnlyRepository unitReadOnlyRepository)
    {
        _unitReadOnlyRepository = unitReadOnlyRepository;
    }

    public async Task<IReadOnlyCollection<UnitDto>> Handle(GetUnitsQuery request, CancellationToken cancellationToken)
    {
        var units = await _unitReadOnlyRepository.GetAllAsync().ToListAsync();

        var unitsDto = units.Adapt<IReadOnlyCollection<UnitDto>>();

        return unitsDto;
    }
}
