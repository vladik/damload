﻿using DamLoad.Assets.Entities;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Repositories
{
    public interface IAssetRepository : IDatabaseRepository<AssetEntity>, ISoftDeleteRepository<AssetEntity>, ISortableRepository<AssetEntity>
    {
        Task<List<AssetEntity>> GetAssetsByTag(Guid tagId);
        Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId);
        Task<List<AssetEntity>> GetAssetsByCollection(Guid collectionId);
        Task MoveAssetToFolder(Guid assetId, Guid? newFolderId);
        Task<List<Guid>> GetMetadataIdsAsync(Guid assetId);
        Task<List<Guid>> GetCustomDataIdsAsync(Guid assetId);
        Task<List<Guid>> GetCollectionIdsAsync(Guid assetId);
        Task<List<Guid>> GetTagIdsAsync(Guid assetId);
    }
}
