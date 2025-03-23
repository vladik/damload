using DamLoad.Abstractions.Events;
using DamLoad.Abstractions.Hooks;
using DamLoad.Abstractions.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Assets
{
    public class StartupHook : IStartupHook
    {
        public Task OnStartupAsync(IServiceProvider provider)
        {
            var bus = provider.GetRequiredService<IEventBus>();

            bus.Subscribe<EntityEvent<AssetModel>>(async evt =>
            {
                Console.WriteLine($"📣 Asset created! Status: {evt.Data.Status}");
            });

            return Task.CompletedTask;
        }
    }
}
