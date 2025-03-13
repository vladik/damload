using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;

namespace DamLoad.Assets.Services
{
    public class AssetMetadataService : IAssetMetadataService
    {
        private readonly IAssetMetadataRepository _repository;

        public AssetMetadataService(IAssetMetadataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<AssetMetadataEntity>> GetByAssetIdAsync(Guid assetId) =>
            await _repository.GetByAssetIdAsync(assetId);

        public async Task AddAsync(AssetMetadataEntity metadata) =>
            await _repository.AddAsync(metadata);

        public async Task AddBatchAsync(List<AssetMetadataEntity> metadataList)
        {
            if (metadataList == null || metadataList.Count == 0) return;
            await _repository.AddBatchAsync(metadataList);
        }

        public async Task DeleteByAssetIdAsync(Guid assetId) =>
            await _repository.DeleteByAssetIdAsync(assetId);
    }
}
