
namespace DamLoad.Core.Configurations
{
    public class LocalizationConfig
    {
        public List<string> Locales { get; set; } = new() { "en" }; // Default to English
        public bool LocaleFallback { get; set; } = true;

        public string DefaultLocale => Locales.Count > 0 ? Locales[0] : "en";
    }

    public class StorageConfig
    {
        public string StorageRoot { get; set; } = string.Empty;
        public string CdnBaseUrl { get; set; } = string.Empty;
        public CdnConfig CdnConfiguration { get; set; } = new();
    }

    public class CdnConfig
    {
        public string Provider { get; set; } = "AzureCDN"; // Default to Azure
        public string AccountId { get; set; } = string.Empty; // Subscription ID / AWS Account ID
        public string ResourceId { get; set; } = string.Empty; // Resource Group / AWS CloudFormation Stack
        public string ProfileName { get; set; } = string.Empty; // CDN Profile Name
        public string EndpointName { get; set; } = string.Empty; // CDN Endpoint Name
        public string ApiKey { get; set; } = string.Empty; // Optional API Key for External CDNs
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
        public StorageConfig Storage { get; set; } = new();
        public LocalizationConfig Localization { get; set; } = new();
        public VariantSettings Variants { get; set; } = new();
        public CustomDataConfig CustomData { get; set; } = new();
    }
}
