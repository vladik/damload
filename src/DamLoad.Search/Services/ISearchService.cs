namespace DamLoad.Search.Services;

public interface ISearchService<T>
{
    Task IndexItemAsync(T item);
    Task<List<T>> SearchAsync(string query);
    Task RemoveItemAsync(string id);
}