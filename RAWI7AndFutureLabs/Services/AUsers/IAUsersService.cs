using RAWI7AndFutureLabs.Models;

namespace RAWI7AndFutureLabs.Services.AUsers
{
    public interface IAUsersService
    {
        List<AUser> GetTestUsers();
        Task<bool> AuthenticateUserAsync(string email, string password);
    }
}
