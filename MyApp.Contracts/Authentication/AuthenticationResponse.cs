using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Contracts.Authentication
{
    public record AuthenticationResponse(
        Guid Id,
        string FirstnName,
        string LastName,
        string Email,
        string Token
        );
}
