using DamLoad.Assets.Entities;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Services;

public interface IAssetService : IDatabaseService<AssetEntity>, ISoftDeleteService<AssetEntity>, ISortableService<AssetEntity>
{
    Task<List<AssetEntity>> GetAssetsByCollection(Guid collectionId);
    Task<List<AssetEntity>> GetAssetsByTag(Guid tagId);
    Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId);
    Task<List<AssetEntity>> GetRootAssetsAsync();
    Task MoveAssetToFolder(Guid assetId, Guid? newFolderId);
}