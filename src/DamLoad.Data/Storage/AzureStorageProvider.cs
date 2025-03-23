using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using DamLoad.Core.Configurations;

namespace DamLoad.Data.Storage
{
    public class AzureStorageProvider : IStorageProvider
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _storageRoot;
        private readonly string _cdnBaseUrl;
        private readonly string _accountId;
        private readonly string _resourceId;
        private readonly string _cdnProfileName;
        private readonly string _cdnEndpointName;
        private readonly HttpClient _httpClient;

        public AzureStorageProvider(string storageConnectionString, ConfigurationService configurationService)
        {
            var settings = configurationService.GetSettings()
                ?? throw new ArgumentNullException(nameof(configurationService), "Configuration settings are missing.");

            if (settings.Storage == null)
                throw new ArgumentNullException(nameof(settings.Storage), "Storage settings are missing in damload.config.json.");

            _blobServiceClient = new BlobServiceClient(storageConnectionString);
            _storageRoot = settings.Storage.StorageRoot
                ?? throw new ArgumentNullException(nameof(_storageRoot), "StorageRoot is missing in damload.config.json.");

            _cdnBaseUrl = settings.Storage.CdnBaseUrl
                ?? throw new ArgumentNullException(nameof(_cdnBaseUrl), "CdnBaseUrl is missing in damload.config.json.");
            
            var cdnConfig = settings.Storage.CdnConfiguration;
            if (cdnConfig == null)
                throw new ArgumentNullException(nameof(cdnConfig), "CdnConfiguration section is missing in damload.config.json.");

            _accountId = cdnConfig.AccountId
                ?? throw new ArgumentNullException(nameof(_accountId), "AccountId is missing in damload.config.json.");
            _resourceId = cdnConfig.ResourceId
                ?? throw new ArgumentNullException(nameof(_resourceId), "ResourceId is missing in damload.config.json.");
            _cdnProfileName = cdnConfig.ProfileName
                ?? throw new ArgumentNullException(nameof(_cdnProfileName), "ProfileName is missing in damload.config.json.");
            _cdnEndpointName = cdnConfig.EndpointName
                ?? throw new ArgumentNullException(nameof(_cdnEndpointName), "EndpointName is missing in damload.config.json.");

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> UploadAsync(Stream assetStream, string assetName, string contentType)
        {
            var blobClient = _blobServiceClient
                .GetBlobContainerClient(_storageRoot)
                .GetBlobClient(assetName);

            await blobClient.UploadAsync(assetStream, new Azure.Storage.Blobs.Models.BlobHttpHeaders
            {
                ContentType = contentType
            });

            return assetName;
        }

        public async Task RemoveAsync(string assetName)
        {
            var blobClient = _blobServiceClient
                .GetBlobContainerClient(_storageRoot)
                .GetBlobClient(assetName);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task RenameAsync(string currentName, string updatedName)
        {
            var sourceBlob = _blobServiceClient
                .GetBlobContainerClient(_storageRoot)
                .GetBlobClient(currentName);
            var destBlob = _blobServiceClient
                .GetBlobContainerClient(_storageRoot)
                .GetBlobClient(updatedName);

            await destBlob.StartCopyFromUriAsync(sourceBlob.Uri);
            await RemoveAsync(currentName);
        }

        public Task SetAccessAsync(string assetName, bool isPublic)
        {
            throw new NotImplementedException();
        }

        public Task SetExpirationAsync(string assetName, DateTime expirationDate)
        {
            throw new NotImplementedException();
        }

        public Task CacheCdnUrl(string url)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFromCdnAsync(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
                throw new ArgumentNullException(nameof(assetPath));

            var accessToken = await GetAzureAccessToken();

            var cdnPath = $"/{_storageRoot}/{assetPath}";
            var apiUrl = $"https://management.azure.com/subscriptions/{_accountId}/resourceGroups/{_resourceId}" +
                         $"/providers/Microsoft.Cdn/profiles/{_cdnProfileName}/endpoints/{_cdnEndpointName}/purge?api-version=2024-02-01";
            
            var requestBody = JsonSerializer.Serialize(new { contentPaths = new[] { cdnPath } });
            
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"CDN purge request successful for: {cdnPath}");
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"CDN purge failed: {errorResponse}");
            }
        }

        private async Task<string> GetAzureAccessToken()
        {
            using var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "az",
                    Arguments = "account get-access-token --resource https://management.azure.com --query accessToken -o tsv",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var token = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            return token.Trim();
        }
    }
}
