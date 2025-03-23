using DamLoad.Abstractions.Modules;
using DamLoad.Abstractions.Workflow.Providers;
using DamLoad.Workflow.Configuration;
using DamLoad.Workflow.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Workflow
{
    public class Module : BaseModule, IModuleConfig<WorkflowConfig>
    {
        public override void Register(IServiceCollection services)
        {
            services.AddSingleton<IWorkflowStatusProvider,WorkflowStatusProvider>();
        }
    }
}
