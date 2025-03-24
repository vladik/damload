using DamLoad.Abstractions.Hooks;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Core.Hooks.Shutdown
{
    public static class ShutdownHookRunner
    {
        public static async Task RunAsync(IServiceProvider services)
        {
            var hooks = services.GetServices<IShutdownHook>();
            foreach (var hook in hooks)
            {
                await hook.OnShutdownAsync(services);
            }
        }
    }
}