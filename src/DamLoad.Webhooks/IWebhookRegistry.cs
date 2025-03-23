namespace DamLoad.Webhooks;

public interface IWebhookRegistry
{
    IEnumerable<SubscriptionConfig> GetSubscriptions(string eventType);
}
