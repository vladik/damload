using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;

namespace DamLoad.Assets.Services
{
    public class LocaleService : ILocaleService
    {
        private readonly ILocaleRepository _repository;

        public LocaleService(ILocaleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<LocaleEntity>> GetByAssetIdAndLocaleAsync(Guid assetId, string locale) =>
            await _repository.GetByAssetIdAndLocaleAsync(assetId, locale);

        public async Task<List<LocaleEntity>> GetByLocaleAsync(string locale) =>
            await _repository.GetByLocaleAsync(locale);

        public async Task AddAsync(LocaleEntity locale) =>
            await _repository.AddAsync(locale);

        public async Task AddBatchAsync(List<LocaleEntity> locales)
        {
            if (locales == null || locales.Count == 0) return;
            await _repository.AddBatchAsync(locales);
        }

        public async Task DeleteByAssetIdAsync(Guid assetId) =>
            await _repository.DeleteByAssetIdAsync(assetId);
    }
}
