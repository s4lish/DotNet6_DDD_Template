using Application.Common.Errors;
using Application.Common.Erros2;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthService(IJwtTokenGenerator jwtTokenGenerator,IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public AuthResult Login(string Email, string Password)
        {
            if (_userRepository.GetUserByEmail(Email) is not User user)
            {
                // Flow Control (1/4) user Custom Exceptions. directly send to error route.
                throw new UserNotFoundExceptions();

            }
             
            if(user.Password != Password)
            {
                throw new UserNotFoundExceptions();
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult(user, token);
        }
        //use of OneOf to flow control exceptions
        //oneOf use to have a multi return types !!
        // Flow Control (2/4) OneOf - return error to controller and controller send it to error route with OneOf
        public OneOf<AuthResult, IServiceException> Register(string FirstName, string LastName, string Email, string Password)
        {
            //Check if User already exists
            if (_userRepository.GetUserByEmail(Email) is not null)
            {
                //or

                return new DuplicateEmailExceptions();
            }

            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult(user, token);
        }

        // Flow Control (3/4) Fluent - Result return error to controller and controller send it to error route with Fluent
        public Result<AuthResult> Register2(string FirstName, string LastName, string Email, string Password)
        {
            //Check if User already exists
            if (_userRepository.GetUserByEmail(Email) is not null)
            {
                //or
                // Flow Control (2/4) return error to controller and controller send it to error route with OneOf

                return Result.Fail<AuthResult>(new[] { new DuplicateEmailErrorWithFluent() });
            }

            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult(user, token);
        }
    }
}
