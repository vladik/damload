using DamLoad.Abstractions.Workflow;
using DamLoad.Core.Modules;
using DamLoad.Workflow.Configuration;

public class WorkflowStatusProvider<T> : IWorkflowStatusProvider<T>
{
    private readonly WorkflowDefinition _definition;

    public WorkflowStatusProvider(WorkflowConfig config, ModuleRegistry registry)
    {
        var typeName = typeof(T).FullName!;

        if (!registry.TryGetModuleKeyForType(typeName, out var moduleKey))
            throw new Exception($"No module registered for entity type: {typeName}");

        if (!config.TryGetValue(moduleKey, out var def))
            throw new Exception($"No workflow definition found for module: {moduleKey}");

        _definition = def;
    }

    public string GetDefaultStatus() => _definition.DefaultStatus;

    public IEnumerable<string> GetAllStatuses() => _definition.Statuses.Select(s => s.Name);

    public bool IsValidStatus(string status) =>
        _definition.Statuses.Any(s => s.Name.Equals(status, StringComparison.OrdinalIgnoreCase));
}
