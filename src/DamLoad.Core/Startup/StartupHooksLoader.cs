using DamLoad.Abstractions.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Core.Startup
{
    public static class StartupHooksLoader
    {
        public static async Task<List<Type>> LoadStartupHooks(IServiceProvider services)
        {
            var hooks = services.GetServices<IStartupHook>();
            var executed = new List<Type>();

            foreach (var hook in hooks)
            {
                await hook.OnStartupAsync(services);
                executed.Add(hook.GetType());
            }

            return executed;
        }
    }
}
