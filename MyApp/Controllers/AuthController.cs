using Application.Authentication.Command.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Contracts.Authentication;

namespace MyApp.Controllers
{
    [Route("auth")]
   // [ErrorHandlingFilter]
   // Add Filter For Specific Controller or Add for All in Program.cs
    public class AuthController : ApiController
    {

        //private readonly IMediator _mediator;

        //just use of ISender Interface of Mediator
        private readonly ISender _mediator;

        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public IActionResult Get()
        {
            return Ok("ok");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            //var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

            ErrorOr<AuthResult> res = await _mediator.Send(command);



            return res.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                 errors => Problem(errors)
                );

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);

            //var query = new LoginQuery(request.Email, request.Password);

            ErrorOr<AuthResult> res = await _mediator.Send(query);


            return res.Match(
                 authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                 errors => Problem(errors)
                 );
        }

        //use mapester. dont need anymore..
        //private static AuthenticationResponse MatchResult(AuthResult authResult)
        //{
        //    return new AuthenticationResponse(
        //        authResult.User.Id,
        //        authResult.User.FirstName,
        //        authResult.User.LastName,
        //        authResult.User.Email,
        //        authResult.Token
        //        );
        //}
    }
}
