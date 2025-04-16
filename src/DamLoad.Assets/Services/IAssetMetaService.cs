using DamLoad.Assets.Entities;

public interface IAssetMetaService
{
    Task<List<AssetMetaEntity>> GetByAssetIdAsync(Guid assetId);
    Task AddAsync(AssetMetaEntity meta);
    Task AddBatchAsync(List<AssetMetaEntity> metadataList);
    Task DeleteByAssetIdAsync(Guid assetId);
}
