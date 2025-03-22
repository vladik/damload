namespace DamLoad.Abstractions.Search
{
    public interface ISearchService
    {
        Task IndexAsync(object document);
        Task<IEnumerable<object>> SearchAsync(string query);
    }
}
