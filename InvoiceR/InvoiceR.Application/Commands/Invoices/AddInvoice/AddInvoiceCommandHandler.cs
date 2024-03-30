using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Enums;

namespace InvoiceR.Application.Commands.Invoices.AddInvoice;

internal class AddInvoiceCommandHandler : ICommandHandler<AddInvoiceCommand, InvoiceDetailDto>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InvoiceDetailDto> Handle(AddInvoiceCommand request, CancellationToken cancellationToken)
    {
        var newInvoice = new Invoice()
        {
            Number = await _invoiceRepository.GetNextInvoiceNumberAsync(),
            Date = request.Date,
            CustomerId = request.CustomerId,
            Description = request.Description,
            Net = request.Net,
            Vat = request.Vat,
            Gross = request.Gross,
            CurrencyId = request.CurrencyId,
            Status = request.IsApproved ? InvoiceStatus.Confirmed : InvoiceStatus.Buffer,
            InvoiceItems = request.InvoiceItems.Select(x => new InvoiceItem
            {
                OrdinalNumber = x.OrdinalNumber,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price,
                Net = x.Net,
                Gross = x.Gross,
                CurrencyId = x.CurrencyId,
                VatRateId = x.VatRateId
            }).ToList()
        };

        _invoiceRepository.Add(newInvoice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var invoiceDetailDto = new InvoiceDetailDto()
        {
            Id = newInvoice.Id,
            Number = request.Number,
            Date = request.Date,
            CustomerId = request.CustomerId,
            Description = request.Description,
            Net = request.Net,
            Vat = request.Vat,
            Gross = request.Gross,
        };

        return invoiceDetailDto;
    }
}
