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

        public async Task<ClassifierEntity?> GetByIdAsync(Guid id) => 
            await _repository.GetByIdAsync(id);

        public async Task<ClassifierEntity?> GetBySlugAsync(string slug) => 
            await _repository.GetBySlugAsync(slug);

        public async Task<ClassifierEntity?> GetBySchemeIdAndSlugAsync(Guid schemeId, string slug) =>
            await _repository.GetBySchemeIdAndSlugAsync(schemeId, slug);

        public async Task<List<ClassifierEntity>> GetBySchemeIdAsync(Guid schemeId) =>
            await _repository.GetBySchemeIdAsync(schemeId);

        public async Task<List<ClassifierEntity>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task AddAsync(ClassifierEntity classifier) =>
            await _repository.AddAsync(classifier);

        public async Task UpdateAsync(ClassifierEntity classifier) =>
            await _repository.UpdateAsync(classifier);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}
