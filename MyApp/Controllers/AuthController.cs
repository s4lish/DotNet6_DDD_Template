using Application.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Contracts.Authentication;

namespace MyApp.Controllers
{
    [Route("auth")]
    [ApiController]
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

            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var res = _authService.Login(request.Email, request.Password);

            return Ok(res);
        }
    }
}
