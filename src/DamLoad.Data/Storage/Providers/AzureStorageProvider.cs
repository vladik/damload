using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DamLoad.Data.Storage.Providers
{
    public class AzureStorageProvider : IStorageProvider
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly StorageRootResolver _rootResolver;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public AzureStorageProvider(string connectionString, IConfiguration config, StorageRootResolver rootResolver)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _rootResolver = rootResolver ?? throw new ArgumentNullException(nameof(rootResolver));

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> UploadAsync(Stream stream, string name, string contentType, string root)
        {
            var client = _blobServiceClient.GetBlobContainerClient(root).GetBlobClient(name);

            await client.UploadAsync(stream, new BlobHttpHeaders
            {
                ContentType = contentType
            });

            return name;
        }

        public async Task RemoveAsync(string name, string root)
        {
            var client = _blobServiceClient.GetBlobContainerClient(root).GetBlobClient(name);
            await client.DeleteIfExistsAsync();
        }

        public async Task RenameAsync(string currentName, string updatedName, string root)
        {
            var container = _blobServiceClient.GetBlobContainerClient(root);
            var source = container.GetBlobClient(currentName);
            var dest = container.GetBlobClient(updatedName);

            await dest.StartCopyFromUriAsync(source.Uri);
            await source.DeleteIfExistsAsync();
        }

        public async Task RemoveFromCdnAsync(string cdnPath)
        {
            var token = await GetAzureAccessToken();

            var apiUrl = $"https://management.azure.com/subscriptions/{_config["Storage:CdnConfiguration:AccountId"]}/" +
                         $"resourceGroups/{_config["Storage:CdnConfiguration:ResourceId"]}/providers/Microsoft.Cdn/" +
                         $"profiles/{_config["Storage:CdnConfiguration:ProfileName"]}/endpoints/" +
                         $"{_config["Storage:CdnConfiguration:EndpointName"]}/purge?api-version=2024-02-01";

            var payload = JsonSerializer.Serialize(new { contentPaths = new[] { cdnPath } });
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"CDN purge failed: {error}");
            }
        }

        public async Task MoveAsync(string name, string currentRoot, string status)
        {
            var targetRoot = _rootResolver.ResolveStorageRoot(status);

            if (currentRoot.Equals(targetRoot, StringComparison.OrdinalIgnoreCase))
                return;

            var source = _blobServiceClient.GetBlobContainerClient(currentRoot).GetBlobClient(name);
            var dest = _blobServiceClient.GetBlobContainerClient(targetRoot).GetBlobClient(name);

            await dest.StartCopyFromUriAsync(source.Uri);
            await source.DeleteIfExistsAsync();
        }

        private async Task<string> GetAzureAccessToken()
        {
            using var process = new System.Diagnostics.Process
            {
                StartInfo = new()
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