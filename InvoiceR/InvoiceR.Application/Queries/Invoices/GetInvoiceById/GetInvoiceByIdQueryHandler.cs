using InvoiceR.Application.Dto;
using InvoiceR.Domain.Enums;
using InvoiceR.Application.Configuration.Queries;
using Mapster;
using InvoiceR.Domain.Abstractions.Repositories;

namespace InvoiceR.Application.Queries.Invoices.GetInvoiceById;

public class GetInvoiceByIdQueryHandler : IQueryHandler<GetInvoiceByIdQuery, InvoiceDetailDto>
{
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;

    public GetInvoiceByIdQueryHandler(IInvoiceReadOnlyRepository invoiceReadOnlyRepository)
    {
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
    }

    public async Task<InvoiceDetailDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceReadOnlyRepository.GetByIdWithDetailAsync(request.Id);

        var invoiceDto = invoice.Adapt<InvoiceDetailDto>();

        return invoiceDto;
    }
}
