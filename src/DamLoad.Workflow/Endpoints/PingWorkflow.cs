using FastEndpoints;

namespace DamLoad.Workflow.Endpoints
{
    public class PingWorkflow : EndpointWithoutRequest<string>
    {
        public override void Configure()
        {
            Get("/api/workflow/ping");
            AllowAnonymous();
        }

        public override Task HandleAsync(CancellationToken ct)
            => SendAsync("pong", cancellation: ct);
    }
}
