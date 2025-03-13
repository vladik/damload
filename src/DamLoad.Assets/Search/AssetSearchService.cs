using DamLoad.Assets.Entities;
using DamLoad.Search.Services;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;

namespace DamLoad.Assets.Search;

public class AssetSearchService : SearchService<AssetEntity>
{
    public override async Task IndexItemAsync(AssetEntity asset)
    {
        var doc = new Document
        {
            new StringField("id", asset.Id.ToString(), Field.Store.YES),
            //new TextField("name", asset.Name, Field.Store.YES),
        };

        _writer.UpdateDocument(new Term("id", asset.Id.ToString()), doc);
        _writer.Commit();
        await Task.CompletedTask;
    }

    public override async Task<List<AssetEntity>> SearchAsync(string query)
    {
        using var reader = DirectoryReader.Open(_directory);
        var searcher = new IndexSearcher(reader);
        var parser = new QueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, "name", new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48));
        var parsedQuery = parser.Parse(query);
        var hits = searcher.Search(parsedQuery, 10).ScoreDocs;

        var results = hits.Select(hit =>
        {
            var doc = searcher.Doc(hit.Doc);
            return new AssetEntity
            {
                Id = Guid.Parse(doc.Get("id")),
                //Name = doc.Get("name"),
            };
        }).ToList();

        return await Task.FromResult(results);
    }

    public override async Task RemoveItemAsync(string id)
    {
        _writer.DeleteDocuments(new Term("id", id));
        _writer.Commit();
        await Task.CompletedTask;
    }   
}