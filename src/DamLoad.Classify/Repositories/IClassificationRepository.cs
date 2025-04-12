using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Repositories
{
    public interface IClassificationRepository
    {
        Task<List<ClassificationEntity>> GetByResourceIdAsync(string resourceId);
        Task<List<ClassificationEntity>> GetByClassifierIdAsync(Guid classifierId);
        Task<List<ClassificationEntity>> GetByClassifierSlugAsync(string slug);
        Task AddAsync(ClassificationEntity classification);
        Task DeleteAsync(Guid id);
        Task DeleteByResourceAndClassifierAsync(string resourceId, Guid classifierId);
    }
}
