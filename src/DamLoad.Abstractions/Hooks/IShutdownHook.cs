namespace DamLoad.Abstractions.Hooks
{
    public interface IShutdownHook
    {
        Task OnShutdownAsync(IServiceProvider services);
    }
}
