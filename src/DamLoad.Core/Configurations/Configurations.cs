
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
    public class VariantsConfig
    {
        public bool Enabled { get; set; } = true;
        public List<VariantConfig> CreateVariantsOnUpload { get; set; } = new();
    }

    public class VariantConfig
    {
        public string Name { get; set; } = string.Empty;
        public List<TransformationPresetConfig> Transformations { get; set; } = new();
    }

    public class TransformationPresetConfig
    {
        public string Provider { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public Dictionary<string, object> Parameters { get; set; } = new();
    }

    public class TransformationConfig
    {
        public List<TransformationProvider> Providers { get; set; } = new();
    }

    public class TransformationProvider
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Operations { get; set; } = new();
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

    public class WorkflowConfig
    {
        public string DefaultStatus { get; set; } = "Published";
        public string FinalStatus { get; set; } = "Published";
        public List<WorkflowStatus> Statuses { get; set; } = new();
    }

    public class WorkflowStatus
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Next { get; set; } = new();
        public List<string> Roles { get; set; } = new();
        public bool Approval { get; set; }
        public string StorageRoot { get; set; } = string.Empty;
    }

    public class TasksConfig
    {
        public Dictionary<string, TaskDefinition> TaskDefinitions { get; set; } = new();
    }

    public class TaskDefinition
    {
        public string Type { get; set; } = "cron"; // "cron" or "interval"
        public string Expression { get; set; } = string.Empty; // Cron expression (if applicable)
        public string Interval { get; set; } = "00:00:00"; // Interval time (if applicable)
        public int Priority { get; set; } = 0; // Determines execution order
    }

    public class AppSettings
    {
        public StorageConfig Storage { get; set; } = new();
        public LocalizationConfig Localization { get; set; } = new();
        public CustomDataConfig CustomData { get; set; } = new();
        public WorkflowConfig Workflow { get; set; } = new();
        public VariantsConfig Variants { get; set; } = new();
        public TransformationConfig Transformation { get; set; } = new();
        public TasksConfig Tasks { get; set; } = new();
    }
}
