using FastEndpoints;
using DamLoad.Workflow.Configuration;
using DamLoad.Workflow.Endpoints;

namespace DamLoad.Workflow.Endpoints;
public class GetWorkflowStatuses : Endpoint<GetStatusesRequest, List<string>>
{
    private readonly WorkflowConfig _config;

    public GetWorkflowStatuses(WorkflowConfig config)
    {
        _config = config;
    }

    public override void Configure()
    {
        Get("/api/workflow/{moduleKey}/statuses");
        AllowAnonymous(); // or authorize if needed
    }

    public override Task HandleAsync(GetStatusesRequest req, CancellationToken ct)
    {
        if (!_config.TryGetValue(req.ModuleKey, out var definition))
        {
            return SendNotFoundAsync(ct);
        }

        var statusNames = definition.Statuses.Select(s => s.Name).ToList();
        return SendAsync(statusNames, cancellation: ct);
    }
}