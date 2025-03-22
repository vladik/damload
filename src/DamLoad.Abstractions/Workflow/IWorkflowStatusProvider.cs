namespace DamLoad.Abstractions.Workflow
{
    public interface IWorkflowStatusProvider<T>
    {
        string GetDefaultStatus();
        bool IsValidStatus(string status);
    }
}
