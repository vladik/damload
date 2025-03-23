using DamLoad.Abstractions.Workflow;
using DamLoad.Core.Modules;
using DamLoad.Workflow.Configuration;

public class WorkflowStatusProvider<T> : IWorkflowStatusProvider<T>
{
    private readonly WorkflowDefinition _definition;

    public WorkflowStatusProvider(WorkflowConfig config, ModuleRegistry registry)
    {
        var typeName = typeof(T).FullName!;

        if (!registry.TryGetModuleIdentifierForType(typeName, out var moduleIdentifier))
            throw new Exception($"No module registered for entity type: {typeName}");

        if (!config.TryGetValue(moduleIdentifier, out var def))
            throw new Exception($"No workflow definition found for module: {moduleIdentifier}");

        _definition = def;
    }

    public string GetDefaultStatus() => _definition.DefaultStatus;

    public IEnumerable<string> GetAllStatuses() => _definition.Statuses.Select(s => s.Name);

    public bool IsValidStatus(string status) =>
        _definition.Statuses.Any(s => s.Name.Equals(status, StringComparison.OrdinalIgnoreCase));
}
