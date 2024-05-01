namespace InvoiceR.Domain.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task RegisterAsync(
        string username, 
        string email, 
        string password);
}
