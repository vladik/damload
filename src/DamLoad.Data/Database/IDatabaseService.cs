namespace DamLoad.Data.Database;
public interface IDatabaseService<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, bool includeDeleted = false);
    Task<List<T>> GetAllAsync(bool includeDeleted = false);
    Task<List<T>> GetDeletedOnlyAsync();
    Task<List<T>> GetPagedAsync(int pageNumber, int pageSize, bool includeDeleted = false);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
