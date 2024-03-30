using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Commands.Invoices.RemoveInvoice;

internal class RemoveInvoiceCommandHandler : ICommandHandler<RemoveInvoiceCommand>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);
        if (invoice == null)
            throw new InvoiceNotFoundException(invoice.Id);

        _invoiceRepository.Delete(invoice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
