using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions.Repositories;

namespace InvoiceR.Application.Queries.InvoiceItems.GetInvoiceItemById;
public class GetInvoiceItemByIdQueryHandler : IQueryHandler<GetInvoiceItemByIdQuery, InvoiceItemDetailDto>
{
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;

    public GetInvoiceItemByIdQueryHandler(IInvoiceReadOnlyRepository invoiceReadOnlyRepository)
    {
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
    }

    public async Task<InvoiceItemDetailDto> Handle(GetInvoiceItemByIdQuery request, CancellationToken cancellationToken)
    {
        var invoiceItemDetail = await _invoiceReadOnlyRepository.GetInvoiceItemByIdWithDetailAsync(request.Id);

        var invoiceItemDetailDto = new InvoiceItemDetailDto();
        invoiceItemDetailDto.Id = invoiceItemDetail.Id;
        invoiceItemDetailDto.OrdinalNumber = invoiceItemDetail.OrdinalNumber;
        invoiceItemDetailDto.ProductId = invoiceItemDetail.ProductId;
        invoiceItemDetailDto.Product = invoiceItemDetail.Product.Name;
        invoiceItemDetailDto.Quantity = invoiceItemDetail.Quantity;
        invoiceItemDetailDto.Price = invoiceItemDetail.Price;
        invoiceItemDetailDto.Net = invoiceItemDetail.Net;
        invoiceItemDetailDto.Gross = invoiceItemDetail.Gross;
        invoiceItemDetailDto.CurrencyId = invoiceItemDetail.CurrencyId;
        invoiceItemDetailDto.Currency = invoiceItemDetail.Currency.Symbol;
        invoiceItemDetailDto.VatRateId = invoiceItemDetail.VatRateId;
        invoiceItemDetailDto.VatRate = invoiceItemDetail.VatRate.Symbol;

        return invoiceItemDetailDto;
    }
}
