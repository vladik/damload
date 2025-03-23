namespace DamLoad.Webhooks;

public interface IWebhookDispatcher
{
    Task DispatchAsync(string eventType, object payload);
}