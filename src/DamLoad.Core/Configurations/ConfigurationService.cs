using System.Text.Json;
using System.Text.Json.Serialization;

namespace DamLoad.Core.Configurations
{
    public class ConfigurationService
    {
        private readonly string _configFilePath = "damload.config.json";
        private readonly AppSettings _settings;

        public ConfigurationService()
        {
            _settings = LoadSettings();
        }

        private AppSettings LoadSettings()
        {
            if (File.Exists(_configFilePath))
            {
                var json = File.ReadAllText(_configFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
                return settings ?? new AppSettings();
            }

            var defaultSettings = GetDefaultSettings();
            File.WriteAllText(_configFilePath, JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions { WriteIndented = true }));
            return defaultSettings;
        }

        public AppSettings GetSettings() => _settings;

        private static AppSettings GetDefaultSettings()
        {
            return new AppSettings
            {
                Localization = new LocalizationConfig
                {
                    Locales = new() { "en" },
                    LocaleFallback = true
                },
                Storage = new StorageConfig
                {
                    StorageRoot = string.Empty,
                    CdnBaseUrl = string.Empty,
                    CdnConfiguration = new CdnConfig()
                },
                Workflow = new WorkflowConfig
                {
                    DefaultStatus = "Published",
                    FinalStatus = "Published",
                    Statuses = new()
                    {
                        new WorkflowStatus
                        {
                            Name = "Published",
                            Description = "Final public state (default on upload)",
                            Next = new() { "Unpublished", "Archived" },
                            Approval = false,
                            Roles = new() { "Admin", "Editor" },
                            StorageRoot = "public"
                        }
                    }
                },
                Variants = new VariantsConfig
                {
                    Enabled = false,
                    CreateVariantsOnUpload = new()
                },
                Transformation = new TransformationConfig
                {
                    Providers = new()
                    {
                        new TransformationProvider
                        {
                            Name = "ImageSharp",
                            Operations = new() { "Crop", "Resize", "Rotate" }
                        },
                        new TransformationProvider
                        {
                            Name = "Cloudinary",
                            Operations = new() { "scale", "watermark", "enhance" }
                        },
                        new TransformationProvider
                        {
                            Name = "FFmpeg",
                            Operations = new() { "convert", "trim", "overlay" }
                        }
                    }
                },
                CustomData = new CustomDataConfig
                {
                    Fields = new()
                },
                Tasks = new TasksConfig
                {
                    TaskDefinitions = new()
                    {
                        { "UnpublishExpiredAssets", new TaskDefinition { Type = "cron", Expression = "0 0 * * 1", Priority = 10 } },
                        { "CleanupTemporaryFiles", new TaskDefinition { Type = "interval", Interval = "00:30:00", Priority = 5 } },
                        { "ReindexSearch", new TaskDefinition { Type = "cron", Expression = "0 6 * * *", Priority = 3 } }
                    }
                }
            };
        }
    }
}
