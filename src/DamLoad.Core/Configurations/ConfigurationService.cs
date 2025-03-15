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
                    Variants = new VariantSettings
                    {
                        Enabled = true,
                        CreateVariantsOnUpload = new List<dynamic>
                        {
                            new { Name = "thumb_small", Width = 100, Height = 100, Format = "webp", Quality = 75, Transformation = "crop", Provider = "ImageSharp" },
                            new { Name = "web_standard", Width = 1280, Height = 720, Format = "jpeg", Quality = 85, Transformation = "scale", Provider = "Cloudinary" }
                        },
                        TransformationProviders = new List<string> { "ImageSharp", "Cloudinary", "FFmpeg" }
                    },
                    CustomData = new CustomDataConfig
                    {
                        Fields = new Dictionary<string, List<CustomFieldConfig>>
                        {
                            { "Image", new List<CustomFieldConfig> { new CustomFieldConfig { Name = "photographer", IsRequired = false }, new CustomFieldConfig { Name = "resolution", IsRequired = true } } },
                            { "Video", new List<CustomFieldConfig> { new CustomFieldConfig { Name = "director", IsRequired = false }, new CustomFieldConfig { Name = "duration", IsRequired = true } } },
                            { "Raw", new List<CustomFieldConfig> { new CustomFieldConfig { Name = "author", IsRequired = false }, new CustomFieldConfig { Name = "format", IsRequired = false } } }
                        }
                    }
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
            return _settings.CustomData.Fields.ContainsKey(assetType.ToString()) ? _settings.CustomData.Fields[assetType.ToString()] : null;
        }
    }
}
