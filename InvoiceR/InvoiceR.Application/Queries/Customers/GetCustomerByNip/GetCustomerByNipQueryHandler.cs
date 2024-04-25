using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using Mapster;

namespace InvoiceR.Application.Queries.Customers.GetCustomerByNip;

internal class GetCustomerByNipQueryHandler : IQueryHandler<GetCustomerByNipQuery, CustomerDetailDto>
{
    private readonly IGusService _gusService;

    public GetCustomerByNipQueryHandler(IGusService gusService)
    {
        _gusService = gusService;
    }

    public async Task<CustomerDetailDto> Handle(GetCustomerByNipQuery request, CancellationToken cancellationToken)
    {
        string searchResult = await _gusService.GetSearchResultByNipAsync(request.nip);

        return null;
    }
}