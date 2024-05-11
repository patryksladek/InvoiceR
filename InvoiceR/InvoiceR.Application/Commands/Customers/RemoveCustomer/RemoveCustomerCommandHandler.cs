using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Commands.Customers.RemoveCustomer;

internal class RemoveCustomerCommandHandler : ICommandHandler<RemoveCustomerCommand>
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
