using DamLoad.Abstractions.Hooks;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Core.Hooks.Startup
{
    public static class StartupHookRunner
    {
        public static async Task RunAsync(IServiceProvider services)
        {
            var hooks = services.GetServices<IStartupHook>();
            foreach (var hook in hooks)
            {
                await hook.OnStartupAsync(services);
            }
        }
    }
}