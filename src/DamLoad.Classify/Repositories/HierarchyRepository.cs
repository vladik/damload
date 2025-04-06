using DamLoad.Classify.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Classify.Repositories
{
    public class HierarchyRepository : IHierarchyRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public HierarchyRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<HierarchyEntity>> GetChildrenAsync(Guid parentId)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM hierarchies WHERE parent_id = @ParentId ORDER BY sort_order ASC";
            return (await db.QueryAsync<HierarchyEntity>(sql, new { ParentId = parentId })).ToList();
        }

        public async Task AddAsync(HierarchyEntity hierarchy)
        {
            using var db = GetConnection();
            string dbUtcNow = _databaseFactory.GetDbUtcNow();
            string sql = $@"INSERT INTO hierarchies
                (id, parent_id, child_id, sort_order, created_at)
                VALUES
                (@Id, @ParentId, @ChildId, @SortOrder, {dbUtcNow})";
            await db.ExecuteAsync(sql, hierarchy);
        }

        public async Task DeleteAsync(Guid parentId, Guid childId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM hierarchies WHERE parent_id = @ParentId AND child_id = @ChildId";
            await db.ExecuteAsync(sql, new { ParentId = parentId, ChildId = childId });
        }
    }
}
