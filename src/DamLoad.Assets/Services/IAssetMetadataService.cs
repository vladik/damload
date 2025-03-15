using DamLoad.Assets.Entities;

public interface IAssetMetadataService
{
    Task<List<AssetMetadataEntity>> GetByAssetIdAsync(Guid assetId);
    Task AddAsync(AssetMetadataEntity metadata);
    Task AddBatchAsync(List<AssetMetadataEntity> metadataList);
    Task DeleteByAssetIdAsync(Guid assetId);
}
