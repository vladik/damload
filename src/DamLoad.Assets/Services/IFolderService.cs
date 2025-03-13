using DamLoad.Assets.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DamLoad.Assets.Services
{
    public interface IFolderService
    {
        Task<List<FolderEntity>> GetAllFoldersAsync();
        Task<List<FolderEntity>> GetRootFoldersAsync();
        Task<FolderEntity?> GetFolderByIdAsync(Guid folderId);
        Task<bool> FolderExistsAsync(Guid folderId);
        Task AddFolderAsync(FolderEntity folder);
        Task RenameFolderAsync(Guid folderId, string newName);
        Task DeleteFolderAsync(Guid folderId);
        Task UpdateSortOrderAsync(Guid folderId, int newSortOrder);
    }
}
