﻿using System;
using BCrypt.Net;

namespace RAWI7AndFutureLabs.Services.PasswordEncrypt
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        public string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}