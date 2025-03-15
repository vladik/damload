using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Services
{
    public interface ITagService
    {
        Task<List<TagEntity>> GetAllAsync();
        Task AddAsync(TagEntity tag);
        Task RenameAsync(Guid tagId, string newName);
        Task DeleteAsync(Guid tagId);
    }
}
