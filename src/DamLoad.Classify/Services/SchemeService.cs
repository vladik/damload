using DamLoad.Classify.Entities;
using DamLoad.Classify.Repositories;

namespace DamLoad.Classify.Services
{
    public class SchemeService : ISchemeService
    {
        private readonly ISchemeRepository _repository;

        public SchemeService(ISchemeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<SchemeEntity?> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);
        public async Task<List<SchemeEntity>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<SchemeEntity?> GetBySlugAsync(string slug) =>
            await _repository.GetBySlugAsync(slug);

        public async Task AddAsync(SchemeEntity scheme) =>
            await _repository.AddAsync(scheme);

        public async Task UpdateAsync(SchemeEntity scheme) =>
            await _repository.UpdateAsync(scheme);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}
