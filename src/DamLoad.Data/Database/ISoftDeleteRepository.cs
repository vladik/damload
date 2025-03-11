namespace DamLoad.Data.Database;
public interface ISoftDeleteRepository<T> where T : class
{
    Task SoftDeleteAsync(Guid id);
    Task RestoreAsync(Guid id);
}
