namespace DamLoad.Abstractions.Events
{
    public interface IEventBusProvider
    {
        string Provider { get; } // e.g. "kafka", "channel", "azure"
        IEventBus Create(IServiceProvider provider);
    }

}
