namespace DamLoad.Abstractions.Search
{
    public class SearchQuery
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
    }
}
