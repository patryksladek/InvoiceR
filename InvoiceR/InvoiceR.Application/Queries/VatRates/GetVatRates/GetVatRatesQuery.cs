using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.VatRates.GetVatRates;

public record GetVatRatesQuery : IQuery<IReadOnlyCollection<VatRateDto>>;
