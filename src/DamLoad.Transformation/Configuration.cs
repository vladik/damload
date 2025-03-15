using DamLoad.Transformation.Providers;
using DamLoad.Transformation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Transformation
{
    public static class Configuration
    {
        public static IServiceCollection AddDamLoadTransformation(this IServiceCollection services)
        {
            services.AddSingleton<ITransformationService, TransformationService>();
            services.AddSingleton<ITransformationProvider, ImageSharpTransformation>();
            services.AddSingleton<ITransformationProvider, CloudinaryTransformation>();
            services.AddSingleton<ITransformationProvider, FFmpegTransformation>();
            return services;
        }
    }
}