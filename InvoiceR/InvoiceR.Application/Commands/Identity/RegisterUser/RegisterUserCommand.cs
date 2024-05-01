using InvoiceR.Application.Configuration.Commands;

namespace InvoiceR.Application.Commands.Identity.RegisterUser;

public class RegisterUserCommand : ICommand
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
