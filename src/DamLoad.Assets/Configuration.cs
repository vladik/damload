using DamLoad.Assets.Contracts;
using DamLoad.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Assets
{
    public static class Configuration
    {
        public static IServiceCollection AddDamLoadAssets(this IServiceCollection services)
        {
            services.AddScoped<IAssetContract, AssetContract>();
            return services;
        }
    }
}