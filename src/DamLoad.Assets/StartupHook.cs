using DamLoad.Abstractions.Events;
using DamLoad.Abstractions.Models;
using DamLoad.Abstractions.Startup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
