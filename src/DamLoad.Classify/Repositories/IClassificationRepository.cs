using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Repositories
{
    public interface IClassificationRepository
    {
        Task<List<ClassificationEntity>> GetByResourceIdAsync(string resourceId);
        Task AddAsync(ClassificationEntity classification);
        Task DeleteAsync(Guid id);
        Task DeleteByResourceAndClassifierAsync(string resourceId, Guid classifierId);
    }
}
