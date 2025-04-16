using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;

namespace DamLoad.Assets.Services
{
    public class AssetMetaService : IAssetMetaService
    {
        private readonly IAssetMetaRepository _repository;

        public AssetMetaService(IAssetMetaRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<AssetMetaEntity>> GetByAssetIdAsync(Guid assetId) =>
            await _repository.GetByAssetIdAsync(assetId);

        public async Task AddAsync(AssetMetaEntity meta) =>
            await _repository.AddAsync(meta);

        public async Task AddBatchAsync(List<AssetMetaEntity> metadataList)
        {
            if (metadataList == null || metadataList.Count == 0) return;
            await _repository.AddBatchAsync(metadataList);
        }

        public async Task DeleteByAssetIdAsync(Guid assetId) =>
            await _repository.DeleteByAssetIdAsync(assetId);
    }
}
