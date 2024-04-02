using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services.AUsers;
namespace RAWI7AndFutureLabs.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<AUser> _users;

        public AuthenticationService(List<AUser> users)
        {
            _users = users;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var user = _users.Find(u => u.Email == email);
            if (user == null || user.EncryptedPassword != password)
                throw new AuthenticationFailedException("Authentication failed. Invalid email or password.");

            Console.WriteLine($"User {email} authenticated successfully.");
            return true;
        }
    }
}
