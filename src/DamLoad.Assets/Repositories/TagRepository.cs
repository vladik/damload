using Dapper;
using DamLoad.Assets.Entities;
using DamLoad.Data.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DamLoad.Assets.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseFactory _databaseFactory;

        public TagRepository(DatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        private IDbConnection GetConnection() => _databaseFactory.CreateConnection();

        public async Task<List<TagEntity>> GetAllAsync()
        {
            using var db = GetConnection();
            string sql = "SELECT * FROM tags ORDER BY name ASC";
            return (await db.QueryAsync<TagEntity>(sql)).ToList();
        }

        public async Task AddAsync(TagEntity tag)
        {
            using var db = GetConnection();
            string sql = "INSERT INTO tags (id, name) VALUES (@Id, @Name)";
            await db.ExecuteAsync(sql, tag);
        }

        public async Task RenameAsync(Guid tagId, string newName)
        {
            using var db = GetConnection();
            string sql = "UPDATE tags SET name = @NewName WHERE id = @TagId";
            await db.ExecuteAsync(sql, new { TagId = tagId, NewName = newName });
        }

        public async Task DeleteAsync(Guid tagId)
        {
            using var db = GetConnection();
            string sql = "DELETE FROM tags WHERE id = @TagId";
            await db.ExecuteAsync(sql, new { TagId = tagId });
        }
    }
}
