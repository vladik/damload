using DamLoad.Abstractions.Hooks;

namespace DamLoad.Assets
{
    public class StartupHook : IStartupHook
    {
        public Task OnStartupAsync(IServiceProvider provider)
        {
            return Task.CompletedTask;
        }
    }
}
