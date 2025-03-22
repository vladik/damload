namespace DamLoad.Abstractions.Events
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IEvent;
        void Subscribe<T>(Func<T, Task> handler) where T : IEvent;
    }
}
