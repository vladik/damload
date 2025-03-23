﻿using DamLoad.Abstractions.Models;
using DamLoad.Abstractions.Modules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DamLoad.Core.Modules
{
    public static class ModuleLoader
    {
        private static readonly ModuleRegistry _registry = new();

        public static List<Assembly> LoadModules(IServiceCollection services)
        {
            var assemblies = new List<Assembly>();

            var basePath = AppContext.BaseDirectory;
            var configFiles = Directory.GetFiles(basePath, "*.config.json", SearchOption.TopDirectoryOnly);

            foreach (var configFile in configFiles)
            {
                var configFileName = Path.GetFileNameWithoutExtension(configFile); // e.g., DamLoad.Assets.config
                var moduleIdentifier = configFileName?.Replace(".config", "", StringComparison.OrdinalIgnoreCase); // DamLoad.Assets

                var dllPath = Path.Combine(basePath, $"{moduleIdentifier}.dll");
                if (!File.Exists(dllPath)) continue;

                var assembly = Assembly.LoadFrom(dllPath);
                assemblies.Add(assembly);

                var moduleTypes = assembly.GetTypes()
                    .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract);

                foreach (var type in moduleTypes)
                {
                    if (Activator.CreateInstance(type) is IModule module)
                    {
                        module.Register(services);

                        // If module also implements IModuleConfig<T>, load and register config
                        var configInterface = type.GetInterfaces()
                            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IModuleConfig<>));

                        if (configInterface is not null)
                        {
                            var configType = configInterface.GetGenericArguments()[0];

                            var loadMethod = typeof(ModuleConfigLoader)
                                .GetMethod(nameof(ModuleConfigLoader.LoadForAssembly))!
                                .MakeGenericMethod(configType);

                            var configInstance = loadMethod.Invoke(null, new object[] { assembly })!;
                            services.AddSingleton(configType, configInstance);
                        }

                        _registry.Register(moduleIdentifier!, new ModuleMetadataModel
                        {
                            Identifier = moduleIdentifier!,
                            ConfigFile = configFile
                        });
                    }
                }

                // Auto-register types that belong to this module (by namespace prefix)
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass && type.Namespace?.StartsWith(moduleIdentifier!, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        _registry.RegisterType(type.FullName!, moduleIdentifier!);
                    }
                }
            }

            services.AddSingleton(_registry);

            return assemblies;
        }
    }
}