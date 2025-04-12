using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Repositories
{
    public interface ISchemeRepository
    {
        Task<SchemeEntity?> GetByIdAsync(Guid id);
        Task<SchemeEntity?> GetBySlugAsync(string slug);
        Task<List<SchemeEntity>> GetAllAsync();
        Task AddAsync(SchemeEntity scheme);
        Task UpdateAsync(SchemeEntity scheme);
        Task DeleteAsync(Guid id);
    }
}
