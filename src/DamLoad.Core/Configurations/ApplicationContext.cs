using DamLoad.Core.Enums;

namespace DamLoad.Core.Configurations
{
    public class ApplicationContext
    {
        private readonly ConfigurationService _configService;
        private AppSettings _settings;
        private event Action? OnSettingsChanged; // Event for UI updates

        public ApplicationContext(ConfigurationService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _settings = _configService.GetSettings();
            CurrentLanguageCode = _settings.Localization.DefaultLocale;
        }

        public string CurrentLanguageCode { get; private set; }
        public List<string> AvailableLocales => _settings.Localization.Locales;
        public bool LocaleFallbackEnabled => _settings.Localization.LocaleFallback;

        public void SetLanguage(string languageCode)
        {
            if (!AvailableLocales.Contains(languageCode))
                throw new InvalidOperationException($"Language '{languageCode}' is not in the available locales.");

            CurrentLanguageCode = languageCode;
            OnSettingsChanged?.Invoke(); // Notify UI components
        }

        public List<CustomFieldConfig>? GetCustomFieldsForAssetType(AssetType assetType)
        {
            return _settings.CustomData.Fields.ContainsKey(assetType.ToString())
                ? _settings.CustomData.Fields[assetType.ToString()]
                : null;
        }

        public async Task UpdateSettingsAsync(AppSettings newSettings)
        {
            _settings = newSettings;
            await _configService.UpdateSettingsAsync(newSettings);
            CurrentLanguageCode = _settings.Localization.DefaultLocale;
            OnSettingsChanged?.Invoke(); // Notify UI components
        }

        public void Subscribe(Action action) => OnSettingsChanged += action;
        public void Unsubscribe(Action action) => OnSettingsChanged -= action;
    }
}
