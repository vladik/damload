using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Repositories
{
    public interface IAssetMetaRepository
    {
        Task<List<AssetMetaEntity>> GetByAssetIdAsync(Guid assetId);
        Task<List<AssetMetaEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale);
        Task AddAsync(AssetMetaEntity meta);
        Task AddBatchAsync(List<AssetMetaEntity> metadataList);
        Task DeleteByAssetIdAsync(Guid assetId);
    }
}
