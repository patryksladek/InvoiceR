namespace InvoiceR.Domain.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task RegisterAsync(
        string email,
        string password);
}
