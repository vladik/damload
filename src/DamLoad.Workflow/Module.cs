using DamLoad.Abstractions.Modules;
using DamLoad.Core.Modules;
using DamLoad.Workflow.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Workflow
{
    public class Module : IModule, IModuleConfig<WorkflowConfig>
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton(typeof(IWorkflowStatusProvider<>), typeof(WorkflowStatusProvider<>));
        }
    }
}
