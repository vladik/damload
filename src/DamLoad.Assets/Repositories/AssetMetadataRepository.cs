using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories
{
    public class AssetMetadataRepository : IAssetMetadataRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public AssetMetadataRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<AssetMetadataEntity>> GetByAssetIdAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM asset_metadata WHERE asset_id = @AssetId";
            return (await db.QueryAsync<AssetMetadataEntity>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task AddAsync(AssetMetadataEntity metadata)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO asset_metadata (id, asset_id, meta_key, meta_value) VALUES (@Id, @AssetId, @MetaKey, @MetaValue)";
            await db.ExecuteAsync(sql, metadata);
        }

        public async Task AddBatchAsync(List<AssetMetadataEntity> metadataList)
        {
            if (metadataList == null || !metadataList.Any()) return; 

            using var db = GetConnection();
            string sql = "INSERT INTO asset_metadata (id, asset_id, meta_key, meta_value) VALUES (@Id, @AssetId, @MetaKey, @MetaValue)";

            await db.ExecuteAsync(sql, metadataList);
        }

        public async Task DeleteByAssetIdAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM asset_metadata WHERE asset_id = @AssetId";
            await db.ExecuteAsync(sql, new { AssetId = assetId });
        }
    }
}
