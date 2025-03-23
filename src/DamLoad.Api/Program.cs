using DamLoad.Core.Modules;
using DamLoad.Core.Startup;
using FastEndpoints;

var builder = WebApplication.CreateBuilder();

var loadedModules = ModuleLoader.LoadModules(builder.Services);

builder.Services.AddFastEndpoints(o => o.Assemblies = loadedModules);

var app = builder.Build();

var loadedHooks = await StartupHooksLoader.LoadStartupHooks(app.Services);

app.UseFastEndpoints();

app.Run();