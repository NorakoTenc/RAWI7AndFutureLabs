using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAWI7AndFutureLabs.Models;
using RAWI7AndFutureLabs.Services;
using RAWI7AndFutureLabs.Services.Auth;
using RAWI7AndFutureLabs.Services.Register;

namespace RAWI7AndFutureLabs.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRegistrationService _userRegistrationService;

        public AuthController(IAuthenticationService authenticationService, IUserRegistrationService userRegistrationService)
        {
            _authenticationService = authenticationService;
            _userRegistrationService = userRegistrationService;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            try
            {
                await _authenticationService.AuthenticateAsync(request.Email, request.Password);
                return Ok("Authentication successful.");
            }
            catch (AuthenticationFailedException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult<AUser>> Register(RegisterRequest request)
        {
            try
            {
                var user = await _userRegistrationService.RegisterAsync(request.AUser, request.Password);
                return CreatedAtAction(nameof(Login), new LoginRequest { Email = user.Email, Password = request.Password }, user);
            }
            catch (UserRegistrationFailedException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterRequest
    {
        public AUser AUser { get; set; }
        public string Password { get; set; }
    }
}
