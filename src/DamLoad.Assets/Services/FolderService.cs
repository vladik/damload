using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;

namespace DamLoad.Assets.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _repository;

        public FolderService(IFolderRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<FolderEntity>> GetAllFoldersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<FolderEntity>> GetRootFoldersAsync() =>
            await _repository.GetRootFoldersAsync();

        public async Task<FolderEntity?> GetFolderByIdAsync(Guid folderId)
        {
            return await _repository.GetFolderByIdAsync(folderId);
        }

        public async Task<bool> FolderExistsAsync(Guid folderId) =>
            await _repository.FolderExistsAsync(folderId);

        public async Task AddFolderAsync(FolderEntity folder) =>
            await _repository.AddFolderAsync(folder);

        public async Task RenameFolderAsync(Guid folderId, string newName) =>
            await _repository.RenameFolderAsync(folderId, newName);

        public async Task DeleteFolderAsync(Guid folderId) =>
            await _repository.DeleteFolderAsync(folderId);
        public async Task UpdateSortOrderAsync(Guid folderId, int newSortOrder) =>
            await _repository.UpdateSortOrderAsync(folderId, newSortOrder);
    }
}
