using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            if (user == null || user.EncryptedPassword != EncryptPassword(password))
                return false;
            return true;
        }
        public List<AUser> GetTestUsers()
        {
            return _users;
        }
    }
}
