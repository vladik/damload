using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Services
{
    public interface IAssetCustomDataService
    {
        Task<List<AssetCustomDataEntity>> GetByAssetIdAsync(Guid assetId);
        Task AddAsync(AssetCustomDataEntity customData);
        Task AddBatchAsync(List<AssetMetadataEntity> metadataList);
        Task UpdateAsync(AssetCustomDataEntity customData);
        Task DeleteByAssetIdAsync(Guid assetId);
    }
}
