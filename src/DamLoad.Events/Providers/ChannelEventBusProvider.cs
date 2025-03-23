using DamLoad.Abstractions.Events;
using DamLoad.Events.Services;

namespace DamLoad.Events.Providers
{
    public class ChannelEventBusProvider : IEventBusProvider
    {
        public string Provider => "channel";

        public IEventBus Create(IServiceProvider provider)
        {
            // Optionally read config, but it's not required for this in-memory implementation
            return new ChannelEventBus();
        }
    }
}
