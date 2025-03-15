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

            return await db.QueryFirstOrDefaultAsync<AssetEntity>(sql, new { Id = id });
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
                INSERT INTO assets (id, public_id, url, public_url, filename, type, bytes, created_at, updated_at)
                VALUES (@Id, @PublicId, @Url, @PublicUrl, @Filename, @Type, @Bytes, {dbUtcNow}, {dbUtcNow})";
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

        public async Task<Guid?> GetFolderIdAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = @"
            SELECT folder_id 
            FROM asset_folder 
            WHERE asset_id = COALESCE(
                (SELECT variant_of_id FROM assets WHERE id = @AssetId), 
                @AssetId
            )";
            return await db.ExecuteScalarAsync<Guid?>(sql, new { AssetId = assetId });
        }

        public async Task MoveAssetToFolder(Guid assetId, Guid? newFolderId)
        {
            using var db = GetConnection();

            if (newFolderId == null)
            {
                // Removing asset from a folder
                string deleteSql = "DELETE FROM asset_folder WHERE asset_id = @AssetId";
                await db.ExecuteAsync(deleteSql, new { AssetId = assetId });
            }
            else
            {
                // Assigning a new folder
                string insertOrUpdateSql = @"
                INSERT INTO asset_folder (asset_id, folder_id) 
                VALUES (@AssetId, @FolderId)
                ON CONFLICT (asset_id) DO UPDATE SET folder_id = EXCLUDED.folder_id";
                await db.ExecuteAsync(insertOrUpdateSql, new { AssetId = assetId, FolderId = newFolderId });
            }
        }

        public async Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId)
        {
            using var db = GetConnection();
            string sql = folderId == null
                ? "SELECT * FROM assets WHERE id NOT IN (SELECT asset_id FROM asset_folder)"
                : "SELECT * FROM assets WHERE id IN (SELECT asset_id FROM asset_folder WHERE folder_id = @FolderId)";
            return (await db.QueryAsync<AssetEntity>(sql, new { FolderId = folderId })).ToList();
        }

        public async Task AssignFolderAsync(Guid assetId, Guid folderId)
        {
            using var db = GetConnection();
            string sql = @"
            INSERT INTO asset_folder (asset_id, folder_id) 
            VALUES (@AssetId, @FolderId)
            ON CONFLICT (asset_id) DO UPDATE SET folder_id = EXCLUDED.folder_id";
            await db.ExecuteAsync(sql, new { AssetId = assetId, FolderId = folderId });
        }

        public async Task RemoveFolderAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM asset_folder WHERE asset_id = @AssetId";
            await db.ExecuteAsync(sql, new { AssetId = assetId });
        }

        public async Task<List<Guid>> GetMetadataIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = @"
        SELECT id FROM asset_metadata 
        WHERE asset_id = COALESCE(
            (SELECT variant_of_id FROM assets WHERE id = @AssetId), 
            @AssetId
        )";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<Guid>> GetCustomDataIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = @"
        SELECT id FROM asset_custom_data 
        WHERE asset_id = COALESCE(
            (SELECT variant_of_id FROM assets WHERE id = @AssetId), 
            @AssetId
        )";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<Guid>> GetCollectionIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = @"
        SELECT collection_id FROM asset_collections 
        WHERE asset_id = COALESCE(
            (SELECT variant_of_id FROM assets WHERE id = @AssetId), 
            @AssetId
        )";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<Guid>> GetTagIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = @"
        SELECT tag_id FROM asset_tags 
        WHERE asset_id = COALESCE(
            (SELECT variant_of_id FROM assets WHERE id = @AssetId), 
            @AssetId
        )";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<AssetEntity>> GetAssetsInFolder(Guid? folderId)
        {
            using var db = GetConnection();

            string sql = folderId == null
                ? "SELECT * FROM assets WHERE id NOT IN (SELECT asset_id FROM asset_folder) AND deleted_at IS NULL"
                : "SELECT * FROM assets WHERE id IN (SELECT asset_id FROM asset_folder WHERE folder_id = @FolderId) AND deleted_at IS NULL";

            return (await db.QueryAsync<AssetEntity>(sql, new { FolderId = folderId })).ToList();
        }

        public async Task UpdateSortOrderAsync(Guid assetId, int newSortOrder)
        {
            using var db = GetConnection();
            string sql = @"
                UPDATE assets 
                SET sort_order = @SortOrder 
                WHERE id = @AssetId AND variant_of_id IS NULL";
            await db.ExecuteAsync(sql, new { AssetId = assetId, SortOrder = newSortOrder });
        }
    }
}
