using RAWI7AndFutureLabs.Models;
namespace RAWI7AndFutureLabs.Services.Register
{
    public class UserRegistrationFailedException : System.Exception
    {
        public UserRegistrationFailedException(string message) : base(message)
        {
        }
    }
}
