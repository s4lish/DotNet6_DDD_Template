using Application.Services.AuthService;
using Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Contracts.Authentication;

namespace MyApp.Controllers
{
    [Route("auth")]
   // [ErrorHandlingFilter]
   // Add Filter For Specific Controller or Add for All in Program.cs
    public class AuthController : ApiController
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
            ErrorOr<AuthResult> res = _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);



            return res.Match(
                authResult => Ok(MatchResult(authResult)),
                 errors => Problem(errors)
                );

        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            ErrorOr<AuthResult> res = _authService.Login(request.Email, request.Password);

            return res.Match(
                 authResult => Ok(MatchResult(authResult)),
                 errors => Problem(errors)
                 );
        }


        private static AuthenticationResponse MatchResult(AuthResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
                );
        }
    }
}
