using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
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
        var vatRates = _vatRateReadOnlyRepository.GetAllAsync();

        var vatRatesDto = await vatRates.Select(x => new VatRateDto()
        {
            Id = x.Id,
            Symbol = x.Symbol,
            Value = x.Value
        })
        .ToListAsync();

        return vatRatesDto;
    }
}
