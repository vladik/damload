namespace DamLoad.Abstractions.Workflow.Providers
{
    public interface IWorkflowStatusProvider
    {
        string GetDefaultStatus(string moduleIdentifier);
        IEnumerable<string> GetAllStatuses(string moduleIdentifier);
        bool IsValidStatus(string moduleIdentifier, string status);
    }
}
