namespace DamLoad.Abstractions.Events
{
    public class EntityEvent<T> : IEvent
    {
        public string EventName { get; set; } = default!; // e.g. "damload.assets:created"
        public T Data { get; set; } = default!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Dictionary<string, string>? Metadata { get; set; }
    }
}
