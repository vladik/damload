using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DamLoad.Webhooks;

public class WebhookDispatcher : IWebhookDispatcher
{
    private readonly IWebhookRegistry _registry;
    private readonly HttpClient _http;

    public WebhookDispatcher(IWebhookRegistry registry, IHttpClientFactory factory)
    {
        _registry = registry;
        _http = factory.CreateClient();
    }

    public async Task DispatchAsync(string eventType, object payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var subscriptions = _registry.GetSubscriptions(eventType);

        foreach (var sub in subscriptions)
        {
            var signature = ComputeHmac(sub.Secret, json);

            var request = new HttpRequestMessage(HttpMethod.Post, sub.TargetUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("X-Damload-Signature", $"sha256={signature}");

            try
            {
                await _http.SendAsync(request);
            }
            catch
            {
                // Optional: Add logging or retry logic
            }
        }
    }

    private static string ComputeHmac(string secret, string json)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(json));
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}