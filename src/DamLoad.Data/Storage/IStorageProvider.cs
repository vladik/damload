namespace DamLoad.Data.Storage;

public interface IStorageProvider
{
    Task<string> UploadAsync(Stream assetStream, string assetName, string contentType);
    Task RemoveAsync(string assetName);
    Task RenameAsync(string currentName, string updatedName);
    Task SetAccessAsync(string assetName, bool isPublic);
    Task SetExpirationAsync(string assetName, DateTime expirationDate);
    Task CacheCdnUrl(string url);
    Task RemoveFromCdnAsync(string url);
}