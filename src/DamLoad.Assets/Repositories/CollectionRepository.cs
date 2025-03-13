using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public CollectionRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<CollectionEntity>> GetAllAsync()
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM collections ORDER BY sort_order ASC";
            return (await db.QueryAsync<CollectionEntity>(sql)).ToList();
        }

        public async Task AddAsync(CollectionEntity collection)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO collections (id, name, sort_order) VALUES (@Id, @Name, @SortOrder)";
            await db.ExecuteAsync(sql, collection);
        }

        public async Task RenameAsync(Guid collectionId, string newName)
        {
            using var db = GetConnection();
            string sql = "UPDATE collections SET name = @NewName WHERE id = @CollectionId";
            await db.ExecuteAsync(sql, new { CollectionId = collectionId, NewName = newName });
        }

        public async Task DeleteAsync(Guid collectionId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM collections WHERE id = @CollectionId";
            await db.ExecuteAsync(sql, new { CollectionId = collectionId });
        }

        public async Task UpdateSortOrderAsync(Guid collectionId, int newSortOrder)
        {
            using var db = GetConnection();
            string sql = "UPDATE collections SET sort_order = @SortOrder WHERE id = @CollectionId";
            await db.ExecuteAsync(sql, new { CollectionId = collectionId, SortOrder = newSortOrder });
        }
    }
}
