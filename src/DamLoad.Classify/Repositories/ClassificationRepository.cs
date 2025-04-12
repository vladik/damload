using DamLoad.Classify.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Classify.Repositories
{
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public ClassificationRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<ClassificationEntity>> GetByResourceIdAsync(string resourceId)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM classify_classifications WHERE resource_id = @ResourceId ORDER BY sort_order ASC";
            return (await db.QueryAsync<ClassificationEntity>(sql, new { ResourceId = resourceId })).ToList();
        }

        public async Task<List<ClassificationEntity>> GetByClassifierIdAsync(Guid classifierId)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM classify_classifications WHERE classifier_id = @ClassifierId";
            return (await db.QueryAsync<ClassificationEntity>(sql, new { ClassifierId = classifierId })).ToList();
        }

        public async Task<List<ClassificationEntity>> GetByClassifierSlugAsync(string slug)
        {
            using var db = GetConnection();
            const string sql = @"
                SELECT c.*
                FROM classify_classifications c
                INNER JOIN classify_classifiers cl ON c.classifier_id = cl.id
                WHERE cl.slug = @Slug
                ORDER BY c.created_at ASC";
            return (await db.QueryAsync<ClassificationEntity>(sql, new { Slug = slug })).ToList();
        }

        public async Task AddAsync(ClassificationEntity classification)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"INSERT INTO classify_classifications
                (id, resource_id, classifier_id, sort_order, created_at)
                VALUES
                (@Id, @ResourceId, @ClassifierId, @SortOrder, {dbUtcNow})";
            await db.ExecuteAsync(sql, classification);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM classify_classifications WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task DeleteByResourceAndClassifierAsync(string resourceId, Guid classifierId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM classify_classifications WHERE resource_id = @ResourceId AND classifier_id = @ClassifierId";
            await db.ExecuteAsync(sql, new { ResourceId = resourceId, ClassifierId = classifierId });
        }
    }
}
