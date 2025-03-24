using DamLoad.Core.Hooks.Shutdown;
using DamLoad.Core.Hooks.Startup;
using DamLoad.Core.Modules;
using DamLoad.Data.Storage;
using FastEndpoints;

var builder = WebApplication.CreateBuilder();

// Storage
builder.Services.AddSingleton<StorageRootResolver>();
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var resolver = sp.GetRequiredService<StorageRootResolver>();
    var connectionString = config.GetConnectionString("StorageConnection");

    return new StorageFactory(connectionString!, config, resolver);
});
builder.Services.AddSingleton(sp => sp.GetRequiredService<StorageFactory>().CreateStorageProvider());

// Modules
var loadedModules = ModuleLoader.LoadModules(builder.Services);

// Api
builder.Services.AddFastEndpoints(o => o.Assemblies = loadedModules);

var app = builder.Build();

// Hooks
await StartupHookRunner.RunAsync(app.Services);
app.Lifetime.ApplicationStopping.Register(() =>
{
    using var scope = app.Services.CreateScope();
    ShutdownHookRunner.RunAsync(scope.ServiceProvider).GetAwaiter().GetResult();
});

app.UseFastEndpoints();

app.Run();