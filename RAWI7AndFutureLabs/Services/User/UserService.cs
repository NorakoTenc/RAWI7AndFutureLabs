using RAWI7AndFutureLabs.Models;
namespace RAWI7AndFutureLabs.Services.User
{
    public class UsersService : IUsersService
    {
        private readonly List<Users> _users;
        public UsersService()
        {
            _users = Enumerable.Range(1, 10).Select(i => new Users
            {
                Id = i,
                Username = $"User{i}",
                Email = $"user{i}@example.com"
            }).ToList();
        }
        public async Task<List<Users>> GetUsersAsync()
        {
            return await Task.FromResult(_users);
        }
        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }
        public async Task<Users> CreateUserAsync(Users user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            return await Task.FromResult(user);
        }
        public async Task<Users> UpdateUserAsync(int id, Users user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
            }
            return await Task.FromResult(existingUser);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                _users.Remove(existingUser);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}