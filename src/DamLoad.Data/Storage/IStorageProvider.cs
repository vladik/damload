namespace DamLoad.Data.Storage;

public interface IStorageProvider
{
    Task<string> UploadAsync(Stream stream, string name, string contentType, string root);
    Task RemoveAsync(string name, string root);
    Task RenameAsync(string currentName, string updatedName, string root);
    Task RemoveFromCdnAsync(string cdnPath);
    Task MoveAsync(string name, string currentRoot, string status);
}