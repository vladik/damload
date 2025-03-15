using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Repositories
{
    public interface IAssetMetadataRepository
    {
        Task<List<AssetMetadataEntity>> GetByAssetIdAsync(Guid assetId);
        Task<List<AssetMetadataEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale);
        Task AddAsync(AssetMetadataEntity metadata);
        Task AddBatchAsync(List<AssetMetadataEntity> metadataList);
        Task DeleteByAssetIdAsync(Guid assetId);
    }
}
