using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Repositories
{
    public interface ITagRepository
    {
        Task<TagEntity?> GetByIdAsync(Guid id);
        Task<List<TagEntity>> GetAllAsync();
        Task AddAsync(TagEntity tag);
        Task RenameAsync(Guid tagId, string newName);
        Task DeleteAsync(Guid tagId);
    }
}
