using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        AuthResult Register(string FirstName,string LastName,string Email,string Password);
        AuthResult Login(string Email,string Password);
    }
}
