using DamLoad.Classify.Entities;

namespace DamLoad.Classify.Services
{
    public interface ISchemeService
    {
        Task<List<SchemeEntity>> GetAllAsync();
        Task<SchemeEntity?> GetBySlugAsync(string slug);
        Task AddAsync(SchemeEntity scheme);
        Task UpdateAsync(SchemeEntity scheme);
        Task DeleteAsync(Guid id);
    }
}
