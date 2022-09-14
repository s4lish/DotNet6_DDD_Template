global using ErrorOr;
global using MediatR;
global using Domain.Common.Errors;
global using Domain.Entities;


using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationRegister
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<IAuthService,AuthService>();

            services.AddMediatR(typeof(ApplicationRegister).Assembly);

            return services;
        }
    }
}
