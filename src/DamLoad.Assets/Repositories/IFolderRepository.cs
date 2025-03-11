using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Repositories;
public interface IFolderRepository
{
    Task<List<FolderEntity>> GetRootFoldersAsync();
    Task<List<FolderEntity>> GetSubfoldersAsync(Guid parentId);
    Task<bool> FolderExistsAsync(Guid folderId);
    Task AddFolderAsync(FolderEntity folder);
    Task RenameFolderAsync(Guid folderId, string newName);
    Task DeleteFolderAsync(Guid folderId);
}
