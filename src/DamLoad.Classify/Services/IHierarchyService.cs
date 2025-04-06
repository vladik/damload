using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Services
{
    public interface IHierarchyService
    {
        Task<List<HierarchyEntity>> GetChildrenAsync(Guid parentId);
        Task AddAsync(HierarchyEntity hierarchy);
        Task DeleteAsync(Guid parentId, Guid childId);
    }
}
