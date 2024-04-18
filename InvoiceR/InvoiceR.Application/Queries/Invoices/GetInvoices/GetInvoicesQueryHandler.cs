using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Invoices.GetInvoices;

public class GetInvoicesQueryHandler : IQueryHandler<GetInvoicesQuery, IReadOnlyCollection<InvoiceDto>>
{
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;

    public GetInvoicesQueryHandler(IInvoiceReadOnlyRepository invoiceReadOnlyRepository)
    {
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
    }

    public async Task<IReadOnlyCollection<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceReadOnlyRepository.GetAllAsync().ToListAsync();

        var invoicesDto = invoices.Adapt<IReadOnlyCollection<InvoiceDto>>();

        return invoicesDto;
    }
}
