using Microsoft.Extensions.Configuration;

namespace DamLoad.Data.Storage
{
    public class StorageRootResolver
    {
        private readonly IConfiguration _config;

        public StorageRootResolver(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public string ResolveStorageRoot(string status)
        {
            var roots = _config.GetSection("storage:storageRoots").Get<List<StorageRoot>>() ?? new();

            var matched = roots
                .FirstOrDefault(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (matched != null)
                return matched.Name;

            var fallbackStatus = _config["storage:defaultStorageRoot"];
            var fallback = roots
                .FirstOrDefault(r => r.Status.Equals(fallbackStatus, StringComparison.OrdinalIgnoreCase));

            if (fallback != null)
                return fallback.Name;

            throw new InvalidOperationException(
                $"No storage root defined for status '{status}', and no fallback defined via 'defaultStorageRoot'.");
        }

        private class StorageRoot
        {
            public string Status { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
}