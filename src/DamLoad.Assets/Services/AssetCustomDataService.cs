using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;

namespace DamLoad.Assets.Services
{
    public class AssetCustomDataService : IAssetCustomDataService
    {
        private readonly IAssetCustomDataRepository _repository;

        public AssetCustomDataService(IAssetCustomDataRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<AssetCustomDataEntity>> GetByAssetIdAsync(Guid assetId) =>
            await _repository.GetByAssetIdAsync(assetId);

        public async Task AddAsync(AssetCustomDataEntity customData) =>
            await _repository.AddAsync(customData);

        public async Task AddBatchAsync(List<AssetMetadataEntity> metadataList) =>
            await _repository.AddBatchAsync(metadataList);

        public async Task UpdateAsync(AssetCustomDataEntity customData) =>
            await _repository.UpdateAsync(customData);

        public async Task DeleteByAssetIdAsync(Guid assetId) =>
            await _repository.DeleteByAssetIdAsync(assetId);
    }
}
