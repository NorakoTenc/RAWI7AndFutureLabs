using RAWI7AndFutureLabs.Models;

namespace RAWI7AndFutureLabs.Services.AUsers
{
    public class AUsersService : IAUsersService
    {
private readonly List<AUser> _users;

        public AUsersService()
        {
            _users = new List<AUser>
            {
                new AUser
                {
                    Id = 1,
                    FirstName = "User",
                    LastName = "One",
                    Email = "user.one@email.com",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    EncryptedPassword = EncryptPassword("qwe123"), 
                    LastLoginDate = DateTime.Now,
                    FailedLoginAttempts = 0
                },
                new AUser
                {
                    Id = 2,
                    FirstName = "User",
                    LastName = "Two",
                    Email = "user.Two@email.com",
                    DateOfBirth = new DateTime(1985, 10, 20),
                    EncryptedPassword = EncryptPassword("rty456"),
                    LastLoginDate = DateTime.Now,
                    FailedLoginAttempts = 0
                },
            };
        }
        private string EncryptPassword(string password)
        {
            return password;
        }

        public List<AUser> GetTestUsers()
        {
            return _users;
        }
    }
}
