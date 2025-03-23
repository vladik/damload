using DamLoad.Abstractions.Models;

namespace DamLoad.Core.Modules
{
    public class ModuleRegistry
    {
        private readonly Dictionary<string, ModuleMetadataModel> _moduleMetadata = new();
        private readonly Dictionary<string, string> _typeToModuleName = new();

        public void Register(string moduleIdentifier, ModuleMetadataModel metadata)
        {
            _moduleMetadata[moduleIdentifier] = metadata;
        }

        public void RegisterType(string entityType, string moduleIdentifier)
        {
            _typeToModuleName[entityType] = moduleIdentifier;
        }

        public IReadOnlyDictionary<string, ModuleMetadataModel> GetAllModules() => _moduleMetadata;

        public bool TryGetModuleIdentifierForType(string entityType, out string identifier)
            => _typeToModuleName.TryGetValue(entityType, out identifier!);

        public ModuleMetadataModel? GetModuleMetadata(string identifier)
            => _moduleMetadata.TryGetValue(identifier, out var meta) ? meta : null;
    }

}
