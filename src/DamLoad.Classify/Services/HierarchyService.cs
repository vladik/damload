using DamLoad.Classify.Entities;
using DamLoad.Classify.Repositories;

namespace DamLoad.Classify.Services
{
    public class HierarchyService : IHierarchyService
    {
        private readonly IHierarchyRepository _repository;

        public HierarchyService(IHierarchyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<HierarchyEntity>> GetChildrenAsync(Guid parentId) =>
            await _repository.GetChildrenAsync(parentId);

        public async Task AddAsync(HierarchyEntity hierarchy) =>
            await _repository.AddAsync(hierarchy);

        public async Task DeleteAsync(Guid parentId, Guid childId) =>
            await _repository.DeleteAsync(parentId, childId);
    }
}
