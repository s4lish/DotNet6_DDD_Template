using Application.Authentication.Command.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Mapster;
using MyApp.Contracts.Authentication;

namespace MyApp.Common.Mapping
{
    public class AuthMappingConfig : IRegister
    {
        void IRegister.Register(TypeAdapterConfig config)
        {
            //if 2 model looks like each other dont need to use these.
            //don't need to add these 2 lines but it is good to be.
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthResult, AuthenticationResponse>()
                //.Map(dest => dest.Token, src => src.Token )
                .Map(dest => dest, src => src.User);

        }
    }
}
