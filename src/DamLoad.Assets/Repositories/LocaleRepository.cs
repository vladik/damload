using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories
{
    public class LocaleRepository : ILocaleRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public LocaleRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<LocaleEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM asset_locale_metadata WHERE asset_id = @AssetId AND locale = @Locale";
            return (await db.QueryAsync<LocaleEntity>(sql, new { AssetId = assetId, Locale = locale })).ToList();
        }

        public async Task<List<LocaleEntity>> GetByLocaleAsync(string locale)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM asset_locale_metadata WHERE locale = @Locale";
            return (await db.QueryAsync<LocaleEntity>(sql, new { Locale = locale })).ToList();
        }

        public async Task AddAsync(LocaleEntity locale)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO asset_locale_metadata (id, asset_id, locale, meta_key, meta_value) VALUES (@Id, @AssetId, @Locale, @MetaKey, @MetaValue)";
            await db.ExecuteAsync(sql, locale);
        }

        public async Task AddBatchAsync(List<LocaleEntity> locales)
        {
            if (locales == null || locales.Count == 0) return;

            using var db = GetConnection();
            string sql = "INSERT INTO asset_locale_metadata (id, asset_id, locale, meta_key, meta_value) VALUES (@Id, @AssetId, @Locale, @MetaKey, @MetaValue)";

            await db.ExecuteAsync(sql, locales);
        }

        public async Task DeleteByAssetIdAsync(Guid assetId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM asset_locale_metadata WHERE asset_id = @AssetId";
            await db.ExecuteAsync(sql, new { AssetId = assetId });
        }
    }
}
