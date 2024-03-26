using RAWI7AndFutureLabs.Models;

namespace RAWI7AndFutureLabs.Services.Register
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly List<AUser> _users;

        public UserRegistrationService(List<AUser> users)
        {
            _users = users;
        }
        public async Task<AUser> RegisterAsync(AUser user, string password)
        {
            if (_users.Exists(u => u.Email == user.Email))
                throw new UserRegistrationFailedException("User with the same email already exists.");
            
            user.EncryptedPassword = password;

            _users.Add(user);
            return user;
        }
    }
}
