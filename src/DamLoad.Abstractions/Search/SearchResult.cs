namespace DamLoad.Abstractions.Search
{
    public class SearchResult<T>
    {
        public IEnumerable<T> Documents { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; }
    }
}
