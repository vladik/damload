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
                asset.Metadata = await GetMetadataAsync(asset.Id);
                asset.Locales = await GetLocalesAsync(asset.Id);
                asset.CollectionIds = await GetCollectionIdsAsync(asset.Id);
                asset.TagIds = await GetTagIdsAsync(asset.Id);
            }

            return asset;
        }

        public async Task<List<AssetEntity>> GetAllAsync(bool includeDeleted = false)
        {
            using var db = GetConnection();
            string sql = includeDeleted
                ? "SELECT * FROM assets"
                : "SELECT * FROM assets WHERE deleted_at IS NULL";
            var assets = (await db.QueryAsync<AssetEntity>(sql)).ToList();

            foreach (var asset in assets)
            {
                asset.Metadata = await GetMetadataAsync(asset.Id);
                asset.Locales = await GetLocalesAsync(asset.Id);
                asset.CollectionIds = await GetCollectionIdsAsync(asset.Id);
                asset.TagIds = await GetTagIdsAsync(asset.Id);
            }

            return assets;
        }

        public async Task<List<AssetEntity>> GetDeletedOnlyAsync()
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM assets WHERE deleted_at IS NOT NULL";
            var assets = (await db.QueryAsync<AssetEntity>(sql)).ToList();

            foreach (var asset in assets)
            {
                asset.Metadata = await GetMetadataAsync(asset.Id);
                asset.Locales = await GetLocalesAsync(asset.Id);
                asset.CollectionIds = await GetCollectionIdsAsync(asset.Id);
                asset.TagIds = await GetTagIdsAsync(asset.Id);
            }

            return assets;
        }

        public async Task<List<AssetEntity>> GetPagedAsync(int pageNumber, int pageSize, bool includeDeleted = false)
        {
            using var db = GetConnection();
            string sql = includeDeleted
                ? "SELECT * FROM assets ORDER BY created_at DESC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY"
                : "SELECT * FROM assets WHERE deleted_at IS NULL ORDER BY created_at DESC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";

            var assets = (await db.QueryAsync<AssetEntity>(sql, new
            {
                Limit = pageSize,
                Offset = (pageNumber - 1) * pageSize
            })).ToList();

            foreach (var asset in assets)
            {
                asset.Metadata = await GetMetadataAsync(asset.Id);
                asset.Locales = await GetLocalesAsync(asset.Id);
                asset.CollectionIds = await GetCollectionIdsAsync(asset.Id);
                asset.TagIds = await GetTagIdsAsync(asset.Id);
            }

            return assets;
        }

        private async Task<Dictionary<string, object>> GetMetadataAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT meta_key, meta_value FROM asset_metadata WHERE asset_id = @AssetId";
            var metadata = await db.QueryAsync<(string Key, object Value)>(sql, new { AssetId = assetId });
            return metadata.ToDictionary(x => x.Key, x => x.Value);
        }

        private async Task<Dictionary<string, AssetEntityLocale>> GetLocalesAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT id, locale, name, description FROM asset_locales WHERE asset_id = @AssetId";
            var locales = (await db.QueryAsync<AssetEntityLocale>(sql, new { AssetId = assetId })).ToList();

            foreach (var locale in locales)
            {
                locale.Metadata = await GetLocalizedMetadataAsync(locale.Id);
            }

            return locales.ToDictionary(x => x.Locale, x => x);
        }

        private async Task<Dictionary<string, object>> GetLocalizedMetadataAsync(Guid assetLocaleId)
        {
            using var db = GetConnection();
            string sql = "SELECT meta_key, meta_value FROM asset_locale_metadata WHERE asset_locale_id = @AssetLocaleId";
            var metadata = await db.QueryAsync<(string Key, object Value)>(sql, new { AssetLocaleId = assetLocaleId });
            return metadata.ToDictionary(x => x.Key, x => x.Value);
        }

        private async Task<List<Guid>> GetCollectionIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT collection_id FROM asset_collections WHERE asset_id = @AssetId";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        private async Task<List<Guid>> GetTagIdsAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT tag_id FROM asset_tags WHERE asset_id = @AssetId";
            return (await db.QueryAsync<Guid>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task AddAsync(AssetEntity asset)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO assets (id, public_id, url, public_url, name, description, folder, filename, type, bytes, created_at, updated_at) " +
                         "VALUES (@Id, @PublicId, @Url, @PublicUrl, @Name, @Description, @Folder, @Filename, @Type, @Bytes, GETUTCDATE(), GETUTCDATE())";
            await db.ExecuteAsync(sql, asset);
        }

        public async Task UpdateAsync(AssetEntity asset)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $"UPDATE assets SET name = @Name, description = @Description, url = @Url, public_url = @PublicUrl, updated_at = {dbUtcNow} WHERE id = @Id";
            await db.ExecuteAsync(sql, asset);
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

        public async Task UpdateSortOrderAsync(Guid id, int newSortOrder)
        {
            using var db = GetConnection();
            string sql = "UPDATE collections SET sort_order = @SortOrder WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id, SortOrder = newSortOrder });
        }
        public async Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId)
        {
            using var db = GetConnection();
            string sql = folderId == null
                ? "SELECT * FROM assets WHERE folder_id IS NULL"
                : "SELECT * FROM assets WHERE folder_id = @FolderId";
            return (await db.QueryAsync<AssetEntity>(sql, new { FolderId = folderId })).ToList();
        }

        public async Task MoveAssetToFolder(Guid assetId, Guid? newFolderId)
        {
            using var db = GetConnection();
            string sql = "UPDATE assets SET folder_id = @NewFolderId WHERE id = @AssetId";
            await db.ExecuteAsync(sql, new { AssetId = assetId, NewFolderId = newFolderId });
        }
        public async Task<bool> FolderExistsAsync(Guid folderId)
        {
            using var db = GetConnection();
            string sql = "SELECT COUNT(*) FROM folders WHERE id = @FolderId";
            int count = await db.ExecuteScalarAsync<int>(sql, new { FolderId = folderId });
            return count > 0;
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
    }
}
