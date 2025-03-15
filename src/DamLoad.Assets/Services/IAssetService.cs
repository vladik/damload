using DamLoad.Assets.Entities;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Services;

public interface IAssetService : IDatabaseService<AssetEntity>, ISoftDeleteService<AssetEntity>, ISortableService<AssetEntity>
{
    Task<List<AssetEntity>> GetAssetsByTag(Guid tagId);
    Task<List<AssetEntity>> GetAssetsByCollection(Guid collectionId);
    Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId);
    Task MoveAssetToFolder(Guid assetId, Guid? newFolderId);
    Task<Guid?> GetFolderIdAsync(Guid assetId);
    Task AssignFolderAsync(Guid assetId, Guid folderId);
    Task RemoveFolderAsync(Guid assetId);
    Task<List<AssetEntity>> GetAssetsInFolder(Guid? folderId);
    Task<List<Guid>> GetMetadataIdsAsync(Guid assetId);
    Task<List<Guid>> GetCustomDataIdsAsync(Guid assetId);
    Task<List<Guid>> GetCollectionIdsAsync(Guid assetId);
    Task<List<Guid>> GetTagIdsAsync(Guid assetId);
}