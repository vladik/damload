using DamLoad.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Core
{
    public static class Configuration
    {
        public static IServiceCollection AddDamLoadCore(this IServiceCollection services)
        {
            services.AddSingleton<ConfigurationService>();
            services.AddSingleton<Context>();
            return services;
        }
    }
}