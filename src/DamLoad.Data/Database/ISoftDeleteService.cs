namespace DamLoad.Data.Database;
public interface ISoftDeleteService<T> where T : class
{
    Task SoftDeleteAsync(Guid id);
    Task RestoreAsync(Guid id);
}