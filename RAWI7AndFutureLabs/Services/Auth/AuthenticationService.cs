using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services.AUsers;

namespace RAWI7AndFutureLabs.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAUsersService _aUsersService;

        public AuthenticationService(IAUsersService aUsersService)
        {
            _aUsersService = aUsersService;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {

            var users = _aUsersService.GetTestUsers();

            var user = users.FirstOrDefault(u => u.Email == email);

            if (user == null || user.EncryptedPassword != EncryptPassword(password))
                throw new AuthenticationFailedException("Authentication failed. Invalid email or password.");

            Console.WriteLine($"User {email} authenticated successfully.");
            return true;
        }

        private string EncryptPassword(string password)
        {
            return password;
        }
    }
}
