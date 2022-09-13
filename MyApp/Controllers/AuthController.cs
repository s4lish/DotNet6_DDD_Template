using Application.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Contracts.Authentication;
using MyApp.Filters;

namespace MyApp.Controllers
{
    [Route("auth")]
    [ApiController]
   // [ErrorHandlingFilter]
   // Add Filter For Specific Controller or Add for All in Program.cs
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Get()
        {
            return Ok("ok");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var res = _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            var response = new AuthenticationResponse(res.User.Id, res.User.FirstName, res.User.LastName, res.User.Email, res.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var res = _authService.Login(request.Email, request.Password);

            var response = new AuthenticationResponse(res.User.Id, res.User.FirstName, res.User.LastName, res.User.Email, res.Token);

            return Ok(response);
        }
    }
}
