using DamLoad.Abstractions.Exceptions;
using DamLoad.Classify.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;
using System.Data.Common;

namespace DamLoad.Classify.Repositories
{
    public class SchemeRepository : ISchemeRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public SchemeRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<SchemeEntity?> GetByIdAsync(Guid id)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM classify_schemes WHERE id = @Id";
            return await db.QueryFirstOrDefaultAsync<SchemeEntity>(sql, new { Id = id });
        }

        public async Task<SchemeEntity?> GetBySlugAsync(string slug)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM classify_schemes WHERE slug = @Slug";
            return await db.QueryFirstOrDefaultAsync<SchemeEntity>(sql, new { Slug = slug });
        }

        public async Task<List<SchemeEntity>> GetAllAsync()
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM classify_schemes ORDER BY slug ASC";
            return (await db.QueryAsync<SchemeEntity>(sql)).ToList();
        }

        public async Task AddAsync(SchemeEntity scheme)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"INSERT INTO classify_schemes 
                (id, slug, label, editable, sortable, repeatable, hierarchical, properties, created_at, updated_at) 
                VALUES 
                (@Id, @Slug, @Label, @Editable, @Sortable, @Repeatable, @Hierarchical, @Properties, {dbUtcNow}, {dbUtcNow})";

            try
            {
                await db.ExecuteAsync(sql, scheme);
            }
            catch (DbException ex) when (DatabaseErrorResolver.IsUniqueViolation(ex))
            {
                throw new ConflictException($"A scheme with slug '{scheme.Slug}' already exists.");
            }
        }

        public async Task UpdateAsync(SchemeEntity scheme)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"UPDATE classify_schemes SET
                slug = @Slug,
                label = @Label,
                editable = @Editable,
                sortable = @Sortable,
                repeatable = @Repeatable,
                hierarchical = @Hierarchical,
                properties = @Properties,
                updated_at = {dbUtcNow}
                WHERE id = @Id";
            await db.ExecuteAsync(sql, scheme);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM classify_schemes WHERE id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }
    }
}
