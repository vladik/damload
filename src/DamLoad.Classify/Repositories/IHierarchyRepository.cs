using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Repositories
{
    public interface IHierarchyRepository
    {
        Task<List<HierarchyEntity>> GetChildrenAsync(Guid parentId);
        Task AddAsync(HierarchyEntity hierarchy);
        Task DeleteAsync(Guid parentId, Guid childId);
    }
}
