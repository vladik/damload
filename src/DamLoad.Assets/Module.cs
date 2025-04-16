using DamLoad.Abstractions.Modules;
using DamLoad.Abstractions.Hooks;
using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;
using DamLoad.Assets.Services;
using DamLoad.Data.Database;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Assets
{
    public class Module : BaseModule
    {
        public override void Register(IServiceCollection services)
        {
            services.AddScoped<IDatabaseRepository<AssetEntity>, AssetRepository>();
            services.AddScoped<ISortableRepository<AssetEntity>, AssetRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IDatabaseService<AssetEntity>, AssetService>();
            services.AddScoped<ISortableService<AssetEntity>, AssetService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IAssetMetaRepository, AssetMetaRepository>();
            services.AddScoped<IAssetMetaService, AssetMetaService>();

            services.AddSingleton<IStartupHook, StartupHook>();
        }
    }
}
