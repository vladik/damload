namespace DamLoad.Abstractions.Startup
{
    public interface IStartupHook
    {
        Task OnStartupAsync(IServiceProvider provider);
    }
}
