using DamLoad.Abstractions.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Webhooks
{
    public class Module : BaseModule, IModuleConfig<WebhooksConfig>
    {
        public override void Register(IServiceCollection services)
        {
            
        }
    }
}
