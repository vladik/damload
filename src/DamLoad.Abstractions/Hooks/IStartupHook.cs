namespace DamLoad.Abstractions.Hooks
{
    public interface IStartupHook
    {
        Task OnStartupAsync(IServiceProvider services);
    }
}
