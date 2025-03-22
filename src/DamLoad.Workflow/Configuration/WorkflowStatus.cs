namespace DamLoad.Workflow.Configuration
{
    public class WorkflowStatus
    {
        public string Name { get; set; } = string.Empty;
        public List<string> AllowedTransitions { get; set; } = new();
        public List<string> RolesAllowedToChange { get; set; } = new();
    }
}
