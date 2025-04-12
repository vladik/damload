using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Repositories
{
    public interface IClassifierRepository
    {
        Task<ClassifierEntity?> GetByIdAsync(Guid id);
        Task<ClassifierEntity?> GetBySlugAsync(string slug);
        Task<ClassifierEntity?> GetBySchemeIdAndSlugAsync(Guid schemeId, string slug);
        Task<List<ClassifierEntity>> GetBySchemeIdAsync(Guid schemeId);
        Task<List<ClassifierEntity>> GetAllAsync();
        Task AddAsync(ClassifierEntity classifier);
        Task UpdateAsync(ClassifierEntity classifier);
        Task DeleteAsync(Guid id);
    }
}
