using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using Dapper;
using System.Data;

namespace DamLoad.Assets.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public FolderRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<FolderEntity>> GetAllAsync()
        {
            using var db = GetConnection();
            string sql = "SELECT id, name, parent_id AS ParentId, sort_order AS SortOrder FROM folders ORDER BY sort_order ASC";
            return (await db.QueryAsync<FolderEntity>(sql)).AsList();
        }

        public async Task<FolderEntity?> GetRootFolderAsync()
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM folders WHERE parent_id IS NULL LIMIT 1";
            return await db.QueryFirstOrDefaultAsync<FolderEntity>(sql);
        }

        public async Task<FolderEntity?> GetFolderByIdAsync(Guid folderId)
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM folders WHERE id = @FolderId";
            return await db.QueryFirstOrDefaultAsync<FolderEntity>(sql, new { FolderId = folderId });
        }

        public async Task<bool> FolderExistsAsync(Guid folderId)
        {
            using var db = GetConnection();
            string sql = "SELECT COUNT(*) FROM folders WHERE id = @FolderId";
            int count = await db.ExecuteScalarAsync<int>(sql, new { FolderId = folderId });
            return count > 0;
        }

        public async Task AddFolderAsync(FolderEntity folder)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO folders (id, name, parent_id) VALUES (@Id, @Name, @ParentId)";
            await db.ExecuteAsync(sql, folder);
        }

        public async Task RenameFolderAsync(Guid folderId, string newName)
        {
            using var db = GetConnection();
            string sql = "UPDATE folders SET name = @NewName WHERE id = @FolderId";
            await db.ExecuteAsync(sql, new { FolderId = folderId, NewName = newName });
        }

        public async Task DeleteFolderAsync(Guid folderId)
        {
            using var db = GetConnection();

            string deleteSubfoldersSql = @"
                WITH RECURSIVE Subfolders AS (
                    SELECT id FROM folders WHERE parent_id = @FolderId
                    UNION ALL
                    SELECT f.id FROM folders f
                    INNER JOIN Subfolders s ON f.parent_id = s.id
                )
                DELETE FROM folders WHERE id IN (SELECT id FROM Subfolders);
            ";

            await db.ExecuteAsync(deleteSubfoldersSql, new { FolderId = folderId });

            string deleteFolderSql = "DELETE FROM folders WHERE id = @FolderId";
            await db.ExecuteAsync(deleteFolderSql, new { FolderId = folderId });
        }
        public async Task UpdateSortOrderAsync(Guid folderId, int newSortOrder)
        {
            using var db = GetConnection();
            string sql = "UPDATE folders SET sort_order = @SortOrder WHERE id = @FolderId";
            await db.ExecuteAsync(sql, new { FolderId = folderId, SortOrder = newSortOrder });
        }
    }
}
