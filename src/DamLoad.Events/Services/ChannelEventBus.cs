using DamLoad.Abstractions.Events;
using System.Collections.Concurrent;

namespace DamLoad.Events.Services
{
    public class ChannelEventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, List<Func<IEvent, Task>>> _subscribers = new();

        public Task PublishAsync<T>(T @event) where T : IEvent
        {
            if (_subscribers.TryGetValue(typeof(T), out var handlers))
            {
                var tasks = handlers.Select(h => h(@event));
                return Task.WhenAll(tasks);
            }
            return Task.CompletedTask;
        }

        public void Subscribe<T>(Func<T, Task> handler) where T : IEvent
        {
            var list = _subscribers.GetOrAdd(typeof(T), _ => new());
            list.Add(e => handler((T)e));
        }
    }
}