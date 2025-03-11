using System;
using System.Threading;
using System.Threading.Tasks;
using DamLoad.Assets.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DamLoad.Search;

namespace DamLoad.Assets.Search;
public class AssetSearchWorker : BackgroundService
{
    private readonly IAssetRepository _assetRepository;
    private readonly AssetSearchService _searchIndex;
    private readonly ILogger<AssetSearchWorker> _logger;
    private DateTime _lastSyncTime = DateTime.UtcNow;

    public AssetSearchWorker(IAssetRepository assetRepository, AssetSearchService searchService, ILogger<AssetSearchWorker> logger)
    {
        _assetRepository = assetRepository;
        _searchIndex = searchService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("AssetSearchWorker started. Monitoring asset changes...");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await SyncDatabaseToIndex();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating asset search index: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Sync every 5 minutes
        }
    }

    private async Task SyncDatabaseToIndex()
    {
        _logger.LogInformation($"Checking for asset updates since {_lastSyncTime}...");
        
        //var updatedAssets = await _assetRepository.GetUpdatedAssetsAsync(_lastSyncTime);
        //var deletedAssets = await _assetRepository.GetDeletedAssetsAsync(_lastSyncTime);

        //foreach (var asset in updatedAssets)
        //{
        //    await _searchIndex.IndexItemAsync(asset);
        //}

        //foreach (var assetId in deletedAssets)
        //{
        //    await _searchIndex.RemoveItemAsync(assetId.ToString());
        //}

        //_logger.LogInformation($"Indexed {updatedAssets.Count} new/updated assets, removed {deletedAssets.Count}.");

        _lastSyncTime = DateTime.UtcNow;
    }
}