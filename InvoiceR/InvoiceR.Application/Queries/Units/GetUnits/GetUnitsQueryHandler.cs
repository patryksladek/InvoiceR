using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
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
        var units = _unitReadOnlyRepository.GetAllAsync();

        var unitsDto = await units.Select(x => new UnitDto()
        {
            Id = x.Id,
            Code = x.Code
        })
        .ToListAsync();

        return unitsDto;
    }
}
