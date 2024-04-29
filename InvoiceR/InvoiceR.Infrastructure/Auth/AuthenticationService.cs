using InvoiceR.Domain.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace InvoiceR.Infrastructure.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
    }

    public async Task RegisterAsync(string email, string password)
    {
        IdentityUser user = new IdentityUser()
        {
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = email
        };

        await _userManager.CreateAsync(user, password);
    }
}
