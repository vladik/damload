using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories
{
    public class AssetMetaRepository : IAssetMetaRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public AssetMetaRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<AssetMetaEntity>> GetByAssetIdAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT id FROM asset_metadata WHERE asset_id = @AssetId WHERE asset_id = @AssetId";
            return (await db.QueryAsync<AssetMetaEntity>(sql, new { AssetId = assetId })).ToList();
        }

        public async Task<List<AssetMetaEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM asset_metadata WHERE asset_id = @AssetId AND locale = @Locale";
            return (await db.QueryAsync<AssetMetaEntity>(sql, new { AssetId = assetId, Locale = locale })).AsList();
        }

        public async Task AddAsync(AssetMetaEntity meta)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO asset_metadata (id, asset_id, data_key, data_value) VALUES (@Id, @AssetId, @DataKey, @DataValue)";
            await db.ExecuteAsync(sql, meta);
        }

        public async Task AddBatchAsync(List<AssetMetaEntity> metadataList)
        {
            if (metadataList == null || !metadataList.Any()) return; 

            using var db = GetConnection();
            string sql = "INSERT INTO asset_metadata (id, asset_id, data_key, data_value) VALUES (@Id, @AssetId, @DataKey, @DataValue)";

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
