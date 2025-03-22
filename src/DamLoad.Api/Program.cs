using DamLoad.Core.Modules;
using FastEndpoints;

var builder = WebApplication.CreateBuilder();

var moduleAssemblies = ModuleLoader.LoadModules(builder.Services);

builder.Services.AddFastEndpoints(o => o.Assemblies = moduleAssemblies);

var app = builder.Build();
app.UseFastEndpoints();

app.Run();