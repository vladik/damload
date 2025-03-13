using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public AssetRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<AssetEntity?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            using var db = GetConnection();
            string sql = includeDeleted
                ? "SELECT * FROM assets WHERE id = @Id"
                : "SELECT * FROM assets WHERE id = @Id AND deleted_at IS NULL";

            var asset = await db.QueryFirstOrDefaultAsync<AssetEntity>(sql, new { Id = id });

            if (asset != null)
            {
                asset.MetadataIds = await GetMetadataIdsAsync(asset.Id);
                asset.CustomDataIds = await GetCustomDataIdsAsync(asset.Id);
                asset.CollectionIds = await GetCollectionIdsAsync(asset.Id);
                asset.TagIds = await GetTagIdsAsync(asset.Id);
            }

            return asset;
        }
        public async Task<List<AssetEntity>> GetAllAsync(bool includeDeleted = false)
        {
            using var db = GetConnection();
            string sql = includeDeleted
                ? "SELECT * FROM assets ORDER BY created_at DESC"
                : "SELECT * FROM assets WHERE deleted_at IS NULL ORDER BY created_at DESC";

            return (await db.QueryAsync<AssetEntity>(sql)).ToList();
        }
        public async Task<List<AssetEntity>> GetDeletedOnlyAsync()
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM assets WHERE deleted_at IS NOT NULL ORDER BY created_at DESC";
            return (await db.QueryAsync<AssetEntity>(sql)).ToList();
        }

        public async Task AddAsync(AssetEntity asset)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"
                INSERT INTO assets (id, public_id, url, public_url, folder_id, filename, type, bytes, created_at, updated_at)
                VALUES (@Id, @PublicId, @Url, @PublicUrl, @FolderId, @Filename, @Type, @Bytes, {dbUtcNow}, {dbUtcNow})";
            await db.ExecuteAsync(sql, asset);
        }

        public async Task UpdateAsync(AssetEntity asset)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"
                UPDATE assets 
                SET url = @Url, public_url = @PublicUrl, updated_at = {dbUtcNow}
                WHERE id = @Id";
            await db.ExecuteAsync(sql, asset);
        }

        public async Task<List<AssetEntity>> GetPagedAsync(int pageNumber, int pageSize, bool includeDeleted = false)
        {
            using var db = GetConnection();
            string sql = includeDeleted
                ? "SELECT * FROM assets ORDER BY created_at DESC OFFSET @Offset LIMIT @Limit"
                : "SELECT * FROM assets WHERE deleted_at IS NULL ORDER BY created_at DESC OFFSET @Offset LIMIT @Limit";

            return (await db.QueryAsync<AssetEntity>(sql, new
            {
                Limit = pageSize,
                Offset = (pageNumber - 1) * pageSize
            })).ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM assets WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task UpdateSortOrderAsync(Guid id, int newSortOrder)
        {
            using var db = GetConnection();
            string sql = "UPDATE assets SET sort_order = @SortOrder WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id, SortOrder = newSortOrder });
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $"UPDATE assets SET deleted_at = {dbUtcNow} WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task RestoreAsync(Guid id)
        {
            using var db = GetConnection();
            string sql = "UPDATE assets SET deleted_at = NULL WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<bool> FolderExistsAsync(Guid folderId)
        {
            using var db = GetConnection();
            string sql = "SELECT EXISTS(SELECT 1 FROM folders WHERE id = @FolderId)";
            return await db.ExecuteScalarAsync<bool>(sql, new { FolderId = folderId });
        }

        public async Task<List<Guid>> GetMetadataIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT id FROM asset_metadata WHERE asset_id = @AssetId";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<Guid>> GetCustomDataIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT id FROM asset_custom_data WHERE asset_id = @AssetId";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<Guid>> GetCollectionIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT collection_id FROM asset_collections WHERE asset_id = @AssetId";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<Guid>> GetTagIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT tag_id FROM asset_tags WHERE asset_id = @AssetId";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }
        public async Task<List<AssetEntity>> GetAssetsByCollection(Guid collectionId)
        {
            using var db = GetConnection();
            string sql = @"
                SELECT a.* FROM assets a
                INNER JOIN asset_collections ac ON a.id = ac.asset_id
                WHERE ac.collection_id = @CollectionId
                AND a.deleted_at IS NULL
                ORDER BY a.created_at DESC";

            return (await db.QueryAsync<AssetEntity>(sql, new { CollectionId = collectionId })).ToList();
        }

        public async Task<List<AssetEntity>> GetAssetsByTag(Guid tagId)
        {
            using var db = GetConnection();
            string sql = @"
                SELECT a.* FROM assets a
                INNER JOIN asset_tags atg ON a.id = atg.asset_id
                WHERE atg.tag_id = @TagId
                AND a.deleted_at IS NULL
                ORDER BY a.created_at DESC";

            return (await db.QueryAsync<AssetEntity>(sql, new { TagId = tagId })).ToList();
        }

        public async Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId)
        {
            using var db = GetConnection();
            string sql = folderId == null
                ? "SELECT * FROM assets WHERE folder_id IS NULL AND deleted_at IS NULL"
                : "SELECT * FROM assets WHERE folder_id = @FolderId AND deleted_at IS NULL";

            return (await db.QueryAsync<AssetEntity>(sql, new { FolderId = folderId })).ToList();
        }

        public async Task MoveAssetToFolder(Guid assetId, Guid? newFolderId)
        {
            using var db = GetConnection();
            string sql = "UPDATE assets SET folder_id = @NewFolderId WHERE id = @AssetId AND folder_id != @NewFolderId";
            await db.ExecuteAsync(sql, new { AssetId = assetId, NewFolderId = newFolderId });
        }

    }
}
