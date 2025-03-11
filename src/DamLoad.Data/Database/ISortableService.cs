
namespace DamLoad.Data.Database;
public interface ISortableService<T> where T : class
{
    Task UpdateSortOrderAsync(Guid id, int newSortOrder);
}
