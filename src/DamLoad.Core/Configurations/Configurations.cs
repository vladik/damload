
using DamLoad.Core.Enums;

namespace DamLoad.Core.Configurations
{
    public class LocalizationConfig
    {
        public List<string> Locales { get; set; } = new() { "en" }; // Default to English
        public bool LocaleFallback { get; set; } = true;

        public string DefaultLocale => Locales.Count > 0 ? Locales[0] : "en";
    }

    public class VariantSettings
    {
        public bool Enabled { get; set; } = true;
        public List<dynamic> CreateVariantsOnUpload { get; set; } = new();
        public List<string> TransformationProviders { get; set; } = new();
    }

    public class CustomDataConfig
    {
        public Dictionary<string, List<CustomFieldConfig>> Fields { get; set; } = new();
    }

    public class CustomFieldConfig
    {
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; } = false;
    }

    public class AppSettings
    {
        public LocalizationConfig Localization { get; set; } = new();
        public VariantSettings Variants { get; set; } = new();
        public CustomDataConfig CustomData { get; set; } = new();
    }
}
