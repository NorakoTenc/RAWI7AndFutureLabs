using RAWI7AndFutureLabs.Models;
namespace RAWI7AndFutureLabs.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string email, string password);
    }
}
