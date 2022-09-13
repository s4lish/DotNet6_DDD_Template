using Application.Common.Errors;
using Application.Common.Erros2;
using Application.Services.AuthService;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Contracts.Authentication;
using MyApp.Filters;
using OneOf;

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
            //OneOf<AuthResult,IServiceException> res = _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            Result<AuthResult> res2 = _authService.Register2(request.FirstName, request.LastName, request.Email, request.Password);


            if (res2.IsSuccess)
            {
                return Ok(MapAuthResult(res2.Value));
            }

            var firstError = res2.Errors[0];

            if(firstError is DuplicateEmailErrorWithFluent)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email Already Exist with FluentResult.");
            }

            return Problem();
            //return res.Match(
            //    authResult => Ok(MapAuthResult(authResult)),
            //    error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var res = _authService.Login(request.Email, request.Password);

            var response = new AuthenticationResponse(res.User.Id, res.User.FirstName, res.User.LastName, res.User.Email, res.Token);

            return Ok(response);
        }

        private static AuthenticationResponse MapAuthResult(AuthResult authResult)
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
