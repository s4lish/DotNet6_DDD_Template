using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Persistence;

namespace Infrastructure
{
    public static class InfrastructureRegister
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings._SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
