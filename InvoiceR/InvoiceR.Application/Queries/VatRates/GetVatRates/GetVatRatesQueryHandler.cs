using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.VatRates.GetVatRates;

internal class GetVatRatesQueryHandler : IQueryHandler<GetVatRatesQuery, IReadOnlyCollection<VatRateDto>>
{
    private readonly IVatRateReadOnlyRepository _vatRateReadOnlyRepository;

    public GetVatRatesQueryHandler(IVatRateReadOnlyRepository unitReadOnlyRepository)
    {
        _vatRateReadOnlyRepository = unitReadOnlyRepository;
    }

    public async Task<IReadOnlyCollection<VatRateDto>> Handle(GetVatRatesQuery request, CancellationToken cancellationToken)
    {
        var vatRates = await _vatRateReadOnlyRepository.GetAllAsync().ToListAsync();

        var vatRatesDto = vatRates.Adapt<IReadOnlyCollection<VatRateDto>>();

        return vatRatesDto;
    }
}
