using DamLoad.Core.Configurations;

namespace DamLoad.Core
{
    public class Context
    {
        private readonly ConfigurationService _configService;
        private readonly AppSettings _settings;

        public Context(ConfigurationService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _settings = _configService.GetSettings();
            ContextLocale = _settings.Localization.DefaultLocale;
        }

        public string ContextLocale { get; private set; }
        public List<string> AvailableLocales => _settings.Localization.Locales;
        public bool LocaleFallbackEnabled => _settings.Localization.LocaleFallback;

        public void SetContextLocale(string locale)
        {
            if (!AvailableLocales.Contains(locale))
                throw new InvalidOperationException($"Locale '{locale}' is not in the available locales.");

            ContextLocale = locale;
        }

        public string? ContextAsset { get; private set; }

        public void SetContextAsset(string assetId)
        {
            if (string.IsNullOrWhiteSpace(assetId))
                throw new ArgumentNullException(nameof(assetId), "Asset Id cannot be null or empty.");

            ContextAsset = assetId;
        }
    }
}
