using DamLoad.Classify.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Classify.Repositories
{
    public class ClassifierRepository : IClassifierRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public ClassifierRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<ClassifierEntity?> GetByIdAsync(Guid id)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM classifiers WHERE id = @Id";
            return await db.QueryFirstOrDefaultAsync<ClassifierEntity>(sql, new { Id = id });
        }

        public async Task<List<ClassifierEntity>> GetBySchemeIdAsync(Guid schemeId)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM classifiers WHERE scheme_id = @SchemeId ORDER BY sort_order ASC";
            return (await db.QueryAsync<ClassifierEntity>(sql, new { SchemeId = schemeId })).ToList();
        }

        public async Task AddAsync(ClassifierEntity classifier)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"INSERT INTO classifiers
                (id, scheme_id, slug, label, sort_order, properties, created_at, updated_at)
                VALUES
                (@Id, @SchemeId, @Slug, @Label, @SortOrder, @Properties, {dbUtcNow}, {dbUtcNow})";
            await db.ExecuteAsync(sql, classifier);
        }

        public async Task UpdateAsync(ClassifierEntity classifier)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"UPDATE classifiers SET
                slug = @Slug,
                label = @Label,
                sort_order = @SortOrder,
                properties = @Properties,
                updated_at = {dbUtcNow}
                WHERE id = @Id";
            await db.ExecuteAsync(sql, classifier);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM classifiers WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }
    }
}
