global using OneOf;
global using FluentResults;

using Application.Services.AuthService;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationRegister
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService,AuthService>();

            return services;
        }
    }
}
