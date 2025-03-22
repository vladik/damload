using DamLoad.Abstractions.Models;

namespace DamLoad.Core.Modules
{
    public class ModuleRegistry
    {
        private readonly Dictionary<string, ModuleMetadataModel> _moduleMetadata = new();
        private readonly Dictionary<string, string> _typeToModuleName = new();

        public void Register(string moduleKey, ModuleMetadataModel metadata)
        {
            _moduleMetadata[moduleKey] = metadata;
        }

        public void RegisterType(string entityType, string moduleKey)
        {
            _typeToModuleName[entityType] = moduleKey;
        }

        public IReadOnlyDictionary<string, ModuleMetadataModel> GetAllModules() => _moduleMetadata;

        public bool TryGetModuleKeyForType(string entityType, out string key)
            => _typeToModuleName.TryGetValue(entityType, out key!);

        public ModuleMetadataModel? GetModuleMetadata(string key)
            => _moduleMetadata.TryGetValue(key, out var meta) ? meta : null;
    }

}
