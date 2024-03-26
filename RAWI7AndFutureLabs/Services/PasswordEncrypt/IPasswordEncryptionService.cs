namespace RAWI7AndFutureLabs.Services.PasswordEncrypt
{
    public interface IPasswordEncryptionService
    {
        string EncryptPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
