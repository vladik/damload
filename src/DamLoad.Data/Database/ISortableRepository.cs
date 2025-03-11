namespace DamLoad.Data.Database;
public interface ISortableRepository<T> where T : class
{
    Task UpdateSortOrderAsync(Guid id, int newSortOrder);
}
