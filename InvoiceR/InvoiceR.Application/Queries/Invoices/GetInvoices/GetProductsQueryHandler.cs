using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Enums;
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
        var invoices = _invoiceReadOnlyRepository.GetAllAsync();

        var invoicesDto = await invoices.Select(x => new InvoiceDto()
        {
            Id = x.Id,
            IsApproved = x.Status == InvoiceStatus.Confirmed ? true : false,
            Number = x.Number,
            Date = x.Date,
            Customer = $"{x.Customer.Name}",
            Net = x.Net,
            Vat = x.Vat,
            Gross = x.Gross,
            Currency = x.Currency.Symbol,
            InvoiceItems = x.InvoiceItems.Select(x => new InvoiceItemDto()
            {
                OrdinalNumber = x.OrdinalNumber,
                Product = x.Product.Name,
                Quantity = x.Quantity,
                Price = x.Price,
                Net = x.Net,
                Gross = x.Gross,
                Currency = x.Currency.Symbol
            })
        })
        .ToListAsync();

        return invoicesDto;
    }
}
