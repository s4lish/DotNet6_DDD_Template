

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Common.Errors;
using MyApp.Common.Mapping;

namespace MyApp
{
    public static class PresentationRegister
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, MyAppProblemDetailsFactory>();

            services.AddMapping();

            return services;
        }
    }
}
