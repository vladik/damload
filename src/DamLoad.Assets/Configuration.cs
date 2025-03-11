using DamLoad.Assets.Contracts;
using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;
using DamLoad.Assets.Services;
using DamLoad.Core.Contracts;
using DamLoad.Data.Database;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Assets
{
    public static class Configuration
    {
        public static IServiceCollection AddDamLoadAssets(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseRepository<AssetEntity>, AssetRepository>();
            services.AddScoped<ISoftDeleteRepository<AssetEntity>, AssetRepository>();
            services.AddScoped<ISortableRepository<AssetEntity>, AssetRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IDatabaseService<AssetEntity>, AssetService>();
            services.AddScoped<ISoftDeleteService<AssetEntity>, AssetService>();
            services.AddScoped<ISortableService<AssetEntity>, AssetService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetContract, AssetContract>();
            return services;
        }
    }
}