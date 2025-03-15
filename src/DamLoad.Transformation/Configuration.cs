using DamLoad.Transformation.Providers;
using DamLoad.Transformation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Transformation
{
    public static class Configuration
    {
        public static IServiceCollection AddDamLoadTransformation(this IServiceCollection services)
        {
            services.AddTransient<ITransformationProvider, ImageSharpTransformation>();
            services.AddTransient<ITransformationProvider, CloudinaryTransformation>();
            services.AddTransient<ITransformationProvider, FFmpegTransformation>();
            services.AddSingleton<ITransformationService, TransformationService>();
            return services;
        }
    }
}