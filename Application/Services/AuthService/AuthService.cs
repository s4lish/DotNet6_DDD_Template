using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public AuthResult Login(string Email, string Password)
        {
            return new AuthResult(Guid.NewGuid(), "", "", Email, "Token");
        }

        public AuthResult Register(string FirstName, string LastName, string Email, string Password)
        {
            return new AuthResult(Guid.NewGuid(), FirstName, LastName, Email, "Token");
        }
    }
}
