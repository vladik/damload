using DamLoad.Abstractions.Modules;
using DamLoad.Abstractions.Workflow;
using DamLoad.Workflow.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

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
