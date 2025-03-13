using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Services
{
    public class AssetService : IAssetService, ISoftDeleteService<AssetEntity>, ISortableService<AssetEntity>
    {
        private readonly IAssetRepository _repository;
        private readonly IFolderRepository _folderRepository;
        private readonly IAssetMetadataRepository _metadataRepository;
        private readonly IAssetCustomDataRepository _customDataRepository;
        private readonly ISoftDeleteRepository<AssetEntity> _softDeleteRepository;
        private readonly ISortableRepository<AssetEntity> _sortableRepository;

        public AssetService(
            IAssetRepository repository,
            IFolderRepository folderRepository,
            IAssetMetadataRepository metadataRepository,
            IAssetCustomDataRepository customDataRepository,
            ISoftDeleteRepository<AssetEntity> softDeleteRepository,
            ISortableRepository<AssetEntity> sortableRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _folderRepository = folderRepository ?? throw new ArgumentNullException(nameof(folderRepository));
            _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
            _customDataRepository = customDataRepository ?? throw new ArgumentNullException(nameof(customDataRepository));
            _softDeleteRepository = softDeleteRepository ?? throw new ArgumentNullException(nameof(softDeleteRepository));
            _sortableRepository = sortableRepository ?? throw new ArgumentNullException(nameof(sortableRepository));
        }

        public async Task<AssetEntity?> GetByIdAsync(Guid id, bool includeDeleted = false) =>
            await _repository.GetByIdAsync(id, includeDeleted);

        public async Task<List<AssetMetadataEntity>> GetMetadataByLocaleAsync(Guid assetId, string locale) =>
            await _metadataRepository.GetByAssetIdAndLocaleAsync(assetId, locale);

        public async Task<List<AssetCustomDataEntity>> GetCustomDataByLocaleAsync(Guid assetId, string locale) =>
            await _customDataRepository.GetByAssetIdAndLocaleAsync(assetId, locale);

        public async Task<List<AssetEntity>> GetAllAsync(bool includeDeleted = false) =>
            await _repository.GetAllAsync(includeDeleted);

        public async Task<List<AssetEntity>> GetDeletedOnlyAsync() =>
            await _repository.GetDeletedOnlyAsync();

        public async Task<List<AssetEntity>> GetPagedAsync(int pageNumber, int pageSize, bool includeDeleted = false) =>
            await _repository.GetPagedAsync(pageNumber, pageSize, includeDeleted);

        public async Task AddAsync(AssetEntity asset) =>
            await _repository.AddAsync(asset);

        public async Task UpdateAsync(AssetEntity asset) =>
            await _repository.UpdateAsync(asset);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task SoftDeleteAsync(Guid id) =>
            await _softDeleteRepository.SoftDeleteAsync(id);

        public async Task RestoreAsync(Guid id) =>
            await _softDeleteRepository.RestoreAsync(id);

        public async Task UpdateSortOrderAsync(Guid id, int newSortOrder) =>
            await _sortableRepository.UpdateSortOrderAsync(id, newSortOrder);

        public async Task<List<AssetEntity>> GetAssetsByCollection(Guid collectionId) =>
            await _repository.GetAssetsByCollection(collectionId);

        public async Task<List<AssetEntity>> GetAssetsByTag(Guid tagId) =>
            await _repository.GetAssetsByTag(tagId);

        public async Task<List<AssetEntity>> GetAssetsByFolder(Guid? folderId) =>
            await _repository.GetAssetsByFolder(folderId);

        public async Task MoveAssetToFolder(Guid assetId, Guid? newFolderId)
        {
            if (newFolderId.HasValue)
            {
                var folderExists = await _folderRepository.FolderExistsAsync(newFolderId.Value);
                if (!folderExists)
                    throw new InvalidOperationException("Cannot move asset to a non-existent folder.");
            }

            await _repository.MoveAssetToFolder(assetId, newFolderId);
        }

        public async Task<List<AssetEntity>> GetRootAssetsAsync() =>
            await _repository.GetAssetsByFolder(null);

        public async Task<List<Guid>> GetMetadataIdsAsync(Guid assetId) =>
            await _repository.GetMetadataIdsAsync(assetId);

        public async Task<List<Guid>> GetCustomDataIdsAsync(Guid assetId) =>
            await _repository.GetCustomDataIdsAsync(assetId);

        public async Task<List<Guid>> GetCollectionIdsAsync(Guid assetId) =>
            await _repository.GetCollectionIdsAsync(assetId);

        public async Task<List<Guid>> GetTagIdsAsync(Guid assetId) =>
            await _repository.GetTagIdsAsync(assetId);
    }
}
