using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Services
{
    public interface ILocaleService
    {
        Task<List<LocaleEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale);
        Task<List<LocaleEntity>> GetByLocaleAsync(string locale);
        Task AddAsync(LocaleEntity locale);
        Task AddBatchAsync(List<LocaleEntity> locales);
        Task DeleteByAssetIdAsync(Guid assetId);
    }
}
