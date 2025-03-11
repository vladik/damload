using DamLoad.Assets.Entities;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Repositories
{
    public interface IAssetRepository : IDatabaseRepository<AssetEntity>, ISoftDeleteRepository<AssetEntity>, ISortableRepository<AssetEntity>
    {
        Task<List<AssetEntity>> GetAssetsByCollection(Guid collectionId);
        Task<List<AssetEntity>> GetAssetsByTag(Guid tagId);
        Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId);
        Task MoveAssetToFolder(Guid assetId, Guid? newFolderId);
    }
}
