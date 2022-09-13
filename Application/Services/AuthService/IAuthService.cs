using Application.Common.Errors;
using Application.Common.Erros2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        OneOf<AuthResult,IServiceException> Register(string FirstName,string LastName,string Email,string Password);
        Result<AuthResult> Register2(string FirstName, string LastName, string Email, string Password);

        AuthResult Login(string Email,string Password);
    }
}
