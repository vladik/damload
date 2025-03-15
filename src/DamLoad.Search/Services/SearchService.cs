using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace DamLoad.Search.Services;

public abstract class SearchService<T> : ISearchService<T> where T : class
{
    protected readonly RAMDirectory _directory;
    protected readonly IndexWriter _writer;

    protected SearchService()
    {
        _directory = new RAMDirectory();
        var analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48);
        var config = new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer);
        _writer = new IndexWriter(_directory, config);
    }

    public abstract Task IndexItemAsync(T item);
    public abstract Task<List<T>> SearchAsync(string query);
    public abstract Task RemoveItemAsync(string id);
}