using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Enums;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Commands.Invoices.EditInvoice;

internal class EditInvoiceCommandHandler : ICommandHandler<EditInvoiceCommand>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EditInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);
        if (invoice == null)
            throw new InvoiceNotFoundException(request.Id);

        invoice.Date = request.Date;
        invoice.CustomerId = request.CustomerId;
        invoice.Status = request.IsApproved ? InvoiceStatus.Confirmed : InvoiceStatus.Buffer;
        invoice.Description = request.Description;
        invoice.Net = request.Net;
        invoice.Vat = request.Vat;
        invoice.Gross = request.Gross;

        var requestInvoiceItems = request.InvoiceItems.ToList();
        var invoiceItemsList = invoice.InvoiceItems.ToList();

        foreach (var existingItem in invoiceItemsList)
        {
            var matchingRequestItem = requestInvoiceItems.FirstOrDefault(x => x.Id == existingItem.Id);

            if (matchingRequestItem != null)
            {
                existingItem.OrdinalNumber = matchingRequestItem.OrdinalNumber;
                existingItem.ProductId = matchingRequestItem.ProductId;
                existingItem.Quantity = matchingRequestItem.Quantity;
                existingItem.Price = matchingRequestItem.Price;
                existingItem.Net = matchingRequestItem.Net;
                existingItem.Gross = matchingRequestItem.Gross;
                existingItem.CurrencyId = matchingRequestItem.CurrencyId;
                existingItem.VatRateId = matchingRequestItem.VatRateId;

                requestInvoiceItems.Remove(matchingRequestItem);
            }
            else
            {
                invoice.InvoiceItems.Remove(existingItem);
            }
        }

        foreach (var requestInvoiceItem in requestInvoiceItems)
        {
            var newInvoiceItem = new InvoiceItem
            {
                ProductId = requestInvoiceItem.ProductId,
                Quantity = requestInvoiceItem.Quantity,
                Price = requestInvoiceItem.Price,
                Net = requestInvoiceItem.Net,
                Gross = requestInvoiceItem.Gross,
                CurrencyId = requestInvoiceItem.CurrencyId,
                VatRateId = requestInvoiceItem.VatRateId
            };

            invoice.InvoiceItems.Add(newInvoiceItem);
        }

        _invoiceRepository.Update(invoice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
