namespace DamLoad.Webhooks;

public class WebhookRegistry : IWebhookRegistry
{
    private readonly Dictionary<string, List<SubscriptionConfig>> _subscriptions;

    public WebhookRegistry(WebhooksConfig config)
    {
        _subscriptions = config.Subscriptions
            .GroupBy(x => x.EventType)
            .ToDictionary(
                g => g.Key,
                g => g.ToList()
            );
    }

    public IEnumerable<SubscriptionConfig> GetSubscriptions(string eventType) =>
        _subscriptions.TryGetValue(eventType, out var subs) ? subs : Enumerable.Empty<SubscriptionConfig>();
}