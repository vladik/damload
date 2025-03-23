using DamLoad.Abstractions.Events;
using DamLoad.Abstractions.Modules;
using DamLoad.Events.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Events
{
    public class Module : IModule, IModuleConfig<EventsConfig>
    {
        public void Register(IServiceCollection services)
        {
            // Resolve the appropriate IEventBus from registered IEventBusProvider implementations
            services.AddSingleton<IEventBus>(sp =>
            {
                var config = sp.GetRequiredService<EventsConfig>();
                var providers = sp.GetServices<IEventBusProvider>();

                var selected = providers.FirstOrDefault(p =>
                    p.Provider.Equals(config.Identifier, StringComparison.OrdinalIgnoreCase));

                if (selected is null)
                    throw new InvalidOperationException($"No IEventBusProvider found for mode: {config.Identifier}");

                return selected.Create(sp);
            });

            // Automatically discover and register all IEventBusProvider types in this assembly
            var assembly = typeof(Module).Assembly;

            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IEventBusProvider).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    services.AddSingleton(typeof(IEventBusProvider), type);
                }
            }
        }
    }
}
