
namespace DamLoad.Data.Storage
{
    public class AwsS3StorageProvider : IStorageProvider
    {
        public Task<string> UploadAsync(Stream assetStream, string assetName, string contentType)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string assetName)
        {
            throw new NotImplementedException();
        }

        public Task RenameAsync(string currentName, string updatedName)
        {
            throw new NotImplementedException();
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

        public Task RemoveFromCdnAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}
