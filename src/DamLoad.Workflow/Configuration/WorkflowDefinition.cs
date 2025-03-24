namespace DamLoad.Workflow.Configuration
{
    public class WorkflowDefinition
    {
        public string DefaultStatus { get; set; } = "Published";
        public List<WorkflowStatus> Statuses { get; set; } = new();
    }
}
