using System.Reflection;
using System.Text.Json;

namespace DamLoad.Core.Modules
{
    public static class ModuleConfigLoader
    {
        public static T LoadForAssembly<T>(Assembly assembly) where T : new()
        {
            var moduleKey = assembly.GetName().Name!; // e.g., "DamLoad.Workflow"
            var fileName = $"{moduleKey}.config.json";
            var basePath = AppContext.BaseDirectory;
            var configPath = Path.Combine(basePath, fileName);

            if (!File.Exists(configPath))
                throw new FileNotFoundException($"Config file not found: {configPath}");

            var json = File.ReadAllText(configPath);
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new T();
        }
    }
}