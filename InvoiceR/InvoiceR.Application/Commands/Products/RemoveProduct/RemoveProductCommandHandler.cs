using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Commands.Products.RemoveProduct;

internal class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new ProductNotFoundException(request.Id);

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
