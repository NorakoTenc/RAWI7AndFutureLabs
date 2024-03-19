using RAWI7AndFutureLabs.Models;
namespace RAWI7AndFutureLabs.Services.User
{
    public interface IUsersService
    {
        Task<List<Users>> GetUsersAsync();
        Task<Users> GetUserByIdAsync(int id);
        Task<Users> CreateUserAsync(Users user);
        Task<Users> UpdateUserAsync(int id, Users user);
        Task<bool> DeleteUserAsync(int id);
    }

}
