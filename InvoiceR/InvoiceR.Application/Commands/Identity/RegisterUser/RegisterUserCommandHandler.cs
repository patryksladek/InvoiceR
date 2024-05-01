using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Domain.Abstractions.Authentication;

namespace InvoiceR.Application.Commands.Identity.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        await _authenticationService.RegisterAsync(request.Username, request.Email, request.Password);
    }
}
