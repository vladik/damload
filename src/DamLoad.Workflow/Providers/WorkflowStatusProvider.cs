using DamLoad.Abstractions.Workflow.Providers;
using DamLoad.Workflow.Configuration;

namespace DamLoad.Workflow.Providers;
public class WorkflowStatusProvider : IWorkflowStatusProvider
{
    private readonly WorkflowConfig _config;

    public WorkflowStatusProvider(WorkflowConfig config)
    {
        _config = config;
    }

    public string GetDefaultStatus(string moduleIdentifier)
    {
        var def = GetDefinition(moduleIdentifier);
        return def.DefaultStatus;
    }

    public IEnumerable<string> GetAllStatuses(string moduleIdentifier)
    {
        return GetDefinition(moduleIdentifier).Statuses.Select(s => s.Name);
    }

    public bool IsValidStatus(string moduleIdentifier, string status)
    {
        return GetDefinition(moduleIdentifier)
            .Statuses.Any(s => s.Name.Equals(status, StringComparison.OrdinalIgnoreCase));
    }

    private WorkflowDefinition GetDefinition(string moduleIdentifier)
    {
        if (!_config.TryGetValue(moduleIdentifier, out var def))
            throw new InvalidOperationException($"No workflow defined for module '{moduleIdentifier}'");

        return def;
    }
}