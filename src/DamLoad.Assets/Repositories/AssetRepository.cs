using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories;

public class AssetRepository(DatabaseFactory databaseFactory) : IAssetRepository
{
    private IDbConnection GetConnection() => databaseFactory.CreateConnection();

    public async Task<AssetEntity?> GetByIdAsync(Guid id, bool includeDeleted = false)
    {
        using var db = GetConnection();
        var sql = includeDeleted
            ? "SELECT * FROM assets WHERE id = @Id"
            : "SELECT * FROM assets WHERE id = @Id AND deleted_at IS NULL";

        return await db.QueryFirstOrDefaultAsync<AssetEntity>(sql, new { Id = id });
    }

    public async Task<List<AssetEntity>> GetAllAsync(bool includeDeleted = false)
    {
        using var db = GetConnection();
        var sql = includeDeleted
            ? "SELECT * FROM assets ORDER BY created_at DESC"
            : "SELECT * FROM assets WHERE deleted_at IS NULL ORDER BY created_at DESC";

        return (await db.QueryAsync<AssetEntity>(sql)).ToList();
    }

    public async Task<List<AssetEntity>> GetDeletedOnlyAsync()
    {
        using var db = GetConnection();
        const string sql = "SELECT * FROM assets WHERE deleted_at IS NOT NULL ORDER BY deleted_at DESC";
        return (await db.QueryAsync<AssetEntity>(sql)).ToList();
    }

    public async Task<List<AssetEntity>> GetPagedAsync(int pageNumber, int pageSize, bool includeDeleted = false)
    {
        using var db = GetConnection();
        var sql = includeDeleted
            ? "SELECT * FROM assets ORDER BY created_at DESC OFFSET @Offset LIMIT @Limit"
            : "SELECT * FROM assets WHERE deleted_at IS NULL ORDER BY created_at DESC OFFSET @Offset LIMIT @Limit";

        return (await db.QueryAsync<AssetEntity>(sql, new
        {
            Limit = pageSize,
            Offset = (pageNumber - 1) * pageSize
        })).ToList();
    }
    
    public async Task AddAsync(AssetEntity asset)
    {
        using var db = GetConnection();
        var dbUtcNow = databaseFactory.GetDbUtcNow();

        var sql = $@"
        INSERT INTO assets 
        (id, variant_of_id, public_id, url, public_url, type, content_type, extension, bytes, status, created_at, updated_at)
        VALUES 
        (@Id, @VariantOfId, @PublicId, @Url, @PublicUrl, @Type, @ContentType, @Extension, @Bytes, @Status, {dbUtcNow}, {dbUtcNow})";

        await db.ExecuteAsync(sql, asset);
    }
    
    public async Task UpdateAsync(AssetEntity asset)
    {
        using var db = GetConnection();
        var dbUtcNow = databaseFactory.GetDbUtcNow();

        var sql = $@"
        UPDATE assets
        SET url = @Url,
            public_url = @PublicUrl,
            type = @Type,
            content_type = @ContentType,
            extension = @Extension,
            bytes = @Bytes,
            status = @Status,
            updated_at = {dbUtcNow}
        WHERE id = @Id AND deleted_at IS NULL";

        await db.ExecuteAsync(sql, asset);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        using var db = GetConnection();
        var dbUtcNow = databaseFactory.GetDbUtcNow();

        const string sql = "UPDATE assets SET deleted_at = {dbUtcNow} WHERE id = @Id";
        await db.ExecuteAsync(sql, new { Id = id });
    }

    public async Task UpdateSortOrderAsync(Guid assetId, int newSortOrder)
    {
        using var db = GetConnection();
        const string sql = @"
            UPDATE assets
            SET sort_order = @SortOrder
            WHERE id = @AssetId AND variant_of_id IS NULL";

        await db.ExecuteAsync(sql, new { AssetId = assetId, SortOrder = newSortOrder });
    }
}
