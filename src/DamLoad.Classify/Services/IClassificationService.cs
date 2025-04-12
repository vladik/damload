using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Services
{
    public interface IClassificationService
    {
        Task<List<ClassificationEntity>> GetByResourceIdAsync(string resourceId);
        Task<List<string>> GetResourceIdsByClassifierAsync(Guid classifierId);
        Task<List<string>> GetResourceIdsByClassifierSlugAsync(string slug);
        Task AddAsync(ClassificationEntity classification);
        Task DeleteAsync(Guid id);
        Task DeleteByResourceAndClassifierAsync(string resourceId, Guid classifierId);
        Task<List<Guid>> AssignAsync(string resourceId, List<Guid> classifierIds);
        Task RemoveAsync(string resourceId, List<Guid> classifierIds);
    }
}
