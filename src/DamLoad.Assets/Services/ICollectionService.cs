using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Services
{
    public interface ICollectionService
    {
        Task<List<CollectionEntity>> GetAllAsync();
        Task AddAsync(CollectionEntity collection);
        Task RenameAsync(Guid collectionId, string newName);
        Task DeleteAsync(Guid collectionId);
        Task UpdateSortOrderAsync(Guid collectionId, int newSortOrder);
    }
}
