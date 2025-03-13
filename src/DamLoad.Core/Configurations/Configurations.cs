
using DamLoad.Core.Enums;

namespace DamLoad.Core.Configurations
{
    public class LocalizationConfig
    {
        public List<string> Locales { get; set; } = new() { "en" }; // Default to English
        public bool LocaleFallback { get; set; } = true;

        public string DefaultLocale => Locales.Count > 0 ? Locales[0] : "en";
    }

    public class CustomFieldConfig
    {
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; } = false;
    }

    public class AppSettings
    {
        public LocalizationConfig Localization { get; set; } = new();
        public Dictionary<AssetType, List<CustomFieldConfig>> CustomDataFields { get; set; } = new();
    }
}
