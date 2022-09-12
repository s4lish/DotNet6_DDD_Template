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
                throw new Exception("user does not exist");
            }
             
            if(user.Password != Password)
            {
                throw new Exception("Invalid Password");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult(user, token);
        }

        public AuthResult Register(string FirstName, string LastName, string Email, string Password)
        {
            //Check if User already exists
            if (_userRepository.GetUserByEmail(Email) is not null)
            {
                throw new Exception("user already exists");
            }

            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };

            _userRepository.Add(user);
            //Create User (generate Unique Id) and Persist to DB
            //Create JWT Tokken


            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult(user, token);
        }
    }
}
