using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Services;

public class AssetService(
    IAssetRepository repository,
    ISoftDeleteRepository<AssetEntity> softDeleteRepository,
    ISortableRepository<AssetEntity> sortableRepository)
    : IAssetService
{
    private readonly IAssetRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly ISoftDeleteRepository<AssetEntity> _softDeleteRepository = softDeleteRepository ?? throw new ArgumentNullException(nameof(softDeleteRepository));
    private readonly ISortableRepository<AssetEntity> _sortableRepository = sortableRepository ?? throw new ArgumentNullException(nameof(sortableRepository));

    public Task<AssetEntity?> GetByIdAsync(Guid id, bool includeDeleted = false) =>
        _repository.GetByIdAsync(id, includeDeleted);

    public Task<List<AssetEntity>> GetAllAsync(bool includeDeleted = false) =>
        _repository.GetAllAsync(includeDeleted);

    public Task<List<AssetEntity>> GetDeletedOnlyAsync() =>
        _repository.GetDeletedOnlyAsync();

    public Task<List<AssetEntity>> GetPagedAsync(int pageNumber, int pageSize, bool includeDeleted = false) =>
        _repository.GetPagedAsync(pageNumber, pageSize, includeDeleted);

    public Task AddAsync(AssetEntity asset) =>
        _repository.AddAsync(asset);

    public Task UpdateAsync(AssetEntity asset) =>
        _repository.UpdateAsync(asset);

    public Task DeleteAsync(Guid id) =>
        _repository.DeleteAsync(id);

    public Task UpdateSortOrderAsync(Guid id, int newSortOrder) =>
        _sortableRepository.UpdateSortOrderAsync(id, newSortOrder);
}