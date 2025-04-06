using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Services
{
    public interface IClassifierService
    {
        Task<List<ClassifierEntity>> GetBySchemeIdAsync(Guid schemeId);
        Task AddAsync(ClassifierEntity classifier);
        Task UpdateAsync(ClassifierEntity classifier);
        Task DeleteAsync(Guid id);
    }
}
