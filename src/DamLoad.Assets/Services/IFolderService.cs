using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Services
{
    public interface IFolderService
    {
        Task<List<FolderEntity>> GetAllAsync();
        Task<FolderEntity?> GetRootFolderAsync();
        Task<FolderEntity?> GetFolderByIdAsync(Guid folderId);
        Task<bool> FolderExistsAsync(Guid folderId);
        Task AddFolderAsync(FolderEntity folder);
        Task RenameFolderAsync(Guid folderId, string newName);
        Task DeleteFolderAsync(Guid folderId);
        Task UpdateSortOrderAsync(Guid folderId, int newSortOrder);
    }
}
