using DamLoad.Web.Components;
using Radzen;
using DamLoad.Core;
using DamLoad.Assets;
using DamLoad.Core.Configurations;
using DamLoad.Data.Database;
using DamLoad.Data.Local;
using DamLoad.Data.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.Services.AddDamLoadCore();
builder.Services.AddDamLoadAssets();

var databaseConnectionString = builder.Configuration.GetConnectionString("DatabaseConnection")
    ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
builder.Services.AddSingleton(new DatabaseFactory(databaseConnectionString));

var storageConnectionString = builder.Configuration.GetConnectionString("StorageConnection")
                              ?? throw new InvalidOperationException("Connection string 'StorageConnection' not found.");

builder.Services.AddSingleton(provider =>
{
    var configurationService = provider.GetRequiredService<ConfigurationService>();
    var storageFactory = new StorageFactory(storageConnectionString, configurationService);
    return storageFactory.CreateStorageProvider();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
