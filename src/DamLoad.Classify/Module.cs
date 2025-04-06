using DamLoad.Abstractions.Modules;
using DamLoad.Classify.Repositories;
using DamLoad.Classify.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Classify
{
    public class Module : BaseModule
    {
        public override void Register(IServiceCollection services)
        {
            services.AddScoped<ISchemeRepository, SchemeRepository>();
            services.AddScoped<IClassifierRepository, ClassifierRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IHierarchyRepository, HierarchyRepository>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<IClassifierService, ClassifierService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<IHierarchyService, HierarchyService>();
        }
    }
}
