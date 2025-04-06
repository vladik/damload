using DamLoad.Classify.Entities;
using DamLoad.Classify.Repositories;

namespace DamLoad.Classify.Services
{
    public class ClassifierService : IClassifierService
    {
        private readonly IClassifierRepository _repository;

        public ClassifierService(IClassifierRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<ClassifierEntity>> GetBySchemeIdAsync(Guid schemeId) =>
            await _repository.GetBySchemeIdAsync(schemeId);

        public async Task AddAsync(ClassifierEntity classifier) =>
            await _repository.AddAsync(classifier);

        public async Task UpdateAsync(ClassifierEntity classifier) =>
            await _repository.UpdateAsync(classifier);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}
