using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Repositories
{
    public interface IAssetCustomDataRepository
    {
        Task<List<AssetCustomDataEntity>> GetByAssetIdAsync(Guid assetId);
        Task<List<AssetCustomDataEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale);
        Task AddAsync(AssetCustomDataEntity customData);
        Task AddBatchAsync(List<AssetMetadataEntity> metadataList);
        Task UpdateAsync(AssetCustomDataEntity customData);
        Task DeleteByAssetIdAsync(Guid assetId);
    }
}
