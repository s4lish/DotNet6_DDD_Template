using Mapster;
using MapsterMapper;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MyApp.Common.Mapping
{
    public static class MappingRegister
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();


            return services;
        }
    }
}
