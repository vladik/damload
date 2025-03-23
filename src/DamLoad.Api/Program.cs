using DamLoad.Core.Hooks.Shutdown;
using DamLoad.Core.Hooks.Startup;
using DamLoad.Core.Modules;
using FastEndpoints;

var builder = WebApplication.CreateBuilder();

var loadedModules = ModuleLoader.LoadModules(builder.Services);

builder.Services.AddFastEndpoints(o => o.Assemblies = loadedModules);

var app = builder.Build();

await StartupHookRunner.RunAsync(app.Services);
app.Lifetime.ApplicationStopping.Register(() =>
{
    using var scope = app.Services.CreateScope();
    ShutdownHookRunner.RunAsync(scope.ServiceProvider).GetAwaiter().GetResult();
});

app.UseFastEndpoints();

app.Run();