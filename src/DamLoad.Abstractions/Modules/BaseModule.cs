using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DamLoad.Abstractions.Modules
{
    public abstract class BaseModule : IModule
    {
        public abstract void Register(IServiceCollection services);

        public static string Identifier =>
            Assembly.GetCallingAssembly().GetName().Name!.ToLowerInvariant();
    }
}
