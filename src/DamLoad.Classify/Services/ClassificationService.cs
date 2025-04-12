using DamLoad.Classify.Entities;
using DamLoad.Classify.Repositories;

namespace DamLoad.Classify.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly IClassificationRepository _repository;

        public ClassificationService(IClassificationRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<ClassificationEntity>> GetByResourceIdAsync(string resourceId) =>
            await _repository.GetByResourceIdAsync(resourceId);

        public async Task<List<string>> GetResourceIdsByClassifierAsync(Guid classifierId)
        {
            var all = await _repository.GetByClassifierIdAsync(classifierId);
            return all.Select(c => c.ResourceId).ToList();
        }

        public async Task<List<string>> GetResourceIdsByClassifierSlugAsync(string slug) {
            var all = await _repository.GetByClassifierSlugAsync(slug);
            return all.Select(c => c.ResourceId).ToList();
        }

        public async Task AddAsync(ClassificationEntity classification) =>
            await _repository.AddAsync(classification);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task DeleteByResourceAndClassifierAsync(string resourceId, Guid classifierId) =>
            await _repository.DeleteByResourceAndClassifierAsync(resourceId, classifierId);

        public async Task<List<Guid>> AssignAsync(string resourceId, List<Guid> classifierIds)
        {
            var assigned = new List<Guid>();
            int sortOrder = 0;

            foreach (var classifierId in classifierIds)
            {
                var classification = new ClassificationEntity
                {
                    Id = Guid.NewGuid(),
                    ResourceId = resourceId,
                    ClassifierId = classifierId,
                    SortOrder = sortOrder,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _repository.AddAsync(classification);
                assigned.Add(classifierId);
            }

            return assigned;
        }

        public async Task RemoveAsync(string resourceId, List<Guid> classifierIds)
        {
            foreach (var classifierId in classifierIds)
            {
                await _repository.DeleteByResourceAndClassifierAsync(resourceId, classifierId);
            }
        }
    }
}
