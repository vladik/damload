using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Repositories
{
    public interface IAssetMetadataRepository
    {
        Task<List<AssetMetadataEntity>> GetByAssetIdAsync(Guid assetId);
        Task AddAsync(AssetMetadataEntity metadata);
        Task AddBatchAsync(List<AssetMetadataEntity> metadataList);
        Task DeleteByAssetIdAsync(Guid assetId);
    }
}
