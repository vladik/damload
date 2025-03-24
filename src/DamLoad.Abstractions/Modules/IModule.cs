using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Abstractions.Modules
{
    public interface IModule
    {
        void Register(IServiceCollection services);
    }
}
