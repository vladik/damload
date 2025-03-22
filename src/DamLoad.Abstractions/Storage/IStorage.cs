namespace DamLoad.Abstractions.Storage
{
    public interface IStorage
    {
        Task<string> UploadAsync(string fileName, Stream content);
        Task<Stream> DownloadAsync(string path);
        Task DeleteAsync(string path);
    }
}
