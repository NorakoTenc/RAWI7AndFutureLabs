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
            _users = new List<Users> 
            {
                new Users {Id=1, Username="User1",Email="user1@mail.com"},
                new Users {Id=2, Username="User2",Email="user2@mail.com"},
                new Users {Id=3, Username="User3",Email="user3@mail.com"},
                new Users {Id=4, Username="User4",Email="user4@mail.com"},
                new Users {Id=5, Username="User5",Email="user5@mail.com"},
                new Users {Id=6, Username="User6",Email="user6@mail.com"},
                new Users {Id=7, Username="User7",Email="user7@mail.com"},
                new Users {Id=8, Username="User8",Email="user8@mail.com"},
                new Users {Id=9, Username="User9",Email="user9@mail.com"},
                new Users {Id=10, Username="User10",Email="user10@mail.com"},
            };
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