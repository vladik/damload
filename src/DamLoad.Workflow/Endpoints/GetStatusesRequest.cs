using Microsoft.AspNetCore.Mvc;

namespace DamLoad.Workflow.Endpoints
{
    public class GetStatusesRequest
    {
        [FromRoute]
        public string ModuleKey { get; set; } = string.Empty; // e.g., "damload.assets"
    }

}
