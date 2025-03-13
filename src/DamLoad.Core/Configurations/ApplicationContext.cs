using DamLoad.Core.Enums;

namespace DamLoad.Core.Configurations
{
    public class ApplicationContext
    {
        private readonly ConfigurationService _configService;
        private AppSettings _settings;

        public ApplicationContext(ConfigurationService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _settings = _configService.GetSettings();
            CurrentLocale = _settings.Localization.DefaultLocale;
        }

        public string CurrentLocale { get; private set; }
        public List<string> AvailableLocales => _settings.Localization.Locales;
        public bool LocaleFallbackEnabled => _settings.Localization.LocaleFallback;

        public void SetLanguage(string languageCode)
        {
            if (!AvailableLocales.Contains(languageCode))
                throw new InvalidOperationException($"Language '{languageCode}' is not in the available locales.");

            CurrentLocale = languageCode;
        }

        public List<CustomFieldConfig>? GetCustomFieldsForAssetType(AssetType assetType) =>
            _configService.GetCustomFieldsForAssetType(assetType);

        public async Task UpdateSettingsAsync(AppSettings newSettings)
        {
            _settings = newSettings;
            await _configService.UpdateSettingsAsync(newSettings);

            // Ensure the default language is applied
            CurrentLocale = _settings.Localization.DefaultLocale;
        }
    }
}
