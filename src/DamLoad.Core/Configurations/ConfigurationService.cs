using System.Text.Json;
using System.Text.Json.Serialization;
using DamLoad.Core.Enums;

namespace DamLoad.Core.Configurations
{
    public class ConfigurationService
    {
        private readonly string _configFilePath = "damload.config.json";
        private AppSettings _settings;

        public ConfigurationService()
        {
            _settings = LoadSettings();
        }

        private AppSettings LoadSettings()
        {
            if (!File.Exists(_configFilePath))
            {
                var defaultSettings = new AppSettings
                {
                    Localization = new LocalizationConfig
                    {
                        Locales = new List<string> { "en" },
                        LocaleFallback = true
                    },
                    CustomDataFields = new Dictionary<AssetType, List<CustomFieldConfig>>()
                };

                File.WriteAllText(_configFilePath, JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions { WriteIndented = true }));
                return defaultSettings;
            }

            var json = File.ReadAllText(_configFilePath);
            var settings = JsonSerializer.Deserialize<AppSettings>(json, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });

            return settings ?? new AppSettings();
        }

        public AppSettings GetSettings() => _settings;

        public async Task UpdateSettingsAsync(AppSettings newSettings)
        {
            _settings = newSettings;
            var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            });

            await File.WriteAllTextAsync(_configFilePath, json);
        }

        public string GetDefaultLocale() => _settings.Localization.DefaultLocale;

        public List<CustomFieldConfig>? GetCustomFieldsForAssetType(AssetType assetType)
        {
            return _settings.CustomDataFields.ContainsKey(assetType) ? _settings.CustomDataFields[assetType] : null;
        }
    }
}
