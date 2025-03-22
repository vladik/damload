using DamLoad.Core.Configurations;
using DamLoad.Core.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Core
{
    public static class Configuration
    {
        public static IServiceCollection AddDamLoadCore(this IServiceCollection services)
        {
            var moduleRegistry = new ModuleRegistry();
            services.AddSingleton(moduleRegistry);

            services.AddSingleton<ConfigurationService>();
            services.AddSingleton<Context>();
            return services;
        }
    }
}