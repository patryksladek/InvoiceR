using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Exceptions;
using MediatR;

namespace InvoiceR.Application.Commands.Customers.RemoveCustomer;

internal class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
            throw new CustomerNotFoundException(request.Id);

        _customerRepository.Delete(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
