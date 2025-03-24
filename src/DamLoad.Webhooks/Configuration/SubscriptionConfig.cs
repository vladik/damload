namespace DamLoad.Webhooks;

public class SubscriptionConfig
{
    public string EventType { get; set; } = default!;
    public string TargetUrl { get; set; } = default!;
    public string Secret { get; set; } = default!;
}