using RAWI7AndFutureLabs.Models;

namespace RAWI7AndFutureLabs.Services.Register
{
    public interface IUserRegistrationService
    {
        Task<AUser> RegisterAsync(AUser user, string password);
    }
}
