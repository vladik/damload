namespace DamLoad.Workflow
{
    public interface IWorkflowStatusProvider<T>
    {
        string GetDefaultStatus();
        bool IsValidStatus(string status);
    }
}
