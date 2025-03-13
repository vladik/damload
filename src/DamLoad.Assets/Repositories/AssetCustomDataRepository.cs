using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories
{
    public class AssetCustomDataRepository : IAssetCustomDataRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public AssetCustomDataRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<AssetCustomDataEntity>> GetByAssetIdAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM asset_custom_data WHERE asset_id = @AssetId";
            return (await db.QueryAsync<AssetCustomDataEntity>(sql, new { AssetId = assetId })).AsList();
        }

        public async Task<List<AssetCustomDataEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM asset_custom_data WHERE asset_id = @AssetId AND locale = @Locale";
            return (await db.QueryAsync<AssetCustomDataEntity>(sql, new { AssetId = assetId, Locale = locale })).AsList();
        }

        public async Task AddAsync(AssetCustomDataEntity customData)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO asset_custom_data (id, asset_id, locale, data_key, data_value) VALUES (@Id, @AssetId, @Locale, @DataKey, @DataValue)";
            await db.ExecuteAsync(sql, customData);
        }

        public async Task AddBatchAsync(List<AssetMetadataEntity> metadataList)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO asset_custom_data (id, asset_id, locale, data_key, data_value) VALUES (@Id, @AssetId, @Locale, @DataKey, @DataValue)";
            await db.ExecuteAsync(sql, metadataList);
        }

        public async Task UpdateAsync(AssetCustomDataEntity customData)
        {
            using var db = GetConnection();
            string sql = "UPDATE asset_custom_data SET data_value = @DataValue WHERE id = @Id";
            await db.ExecuteAsync(sql, customData);
        }

        public async Task DeleteByAssetIdAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM asset_custom_data WHERE asset_id = @AssetId";
            await db.ExecuteAsync(sql, new { AssetId = assetId });
        }
    }
}
