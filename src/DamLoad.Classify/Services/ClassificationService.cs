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

        public async Task AddAsync(ClassificationEntity classification) =>
            await _repository.AddAsync(classification);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task DeleteByResourceAndClassifierAsync(string resourceId, Guid classifierId) =>
            await _repository.DeleteByResourceAndClassifierAsync(resourceId, classifierId);
    }
}
