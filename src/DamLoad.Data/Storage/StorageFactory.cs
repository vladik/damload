using DamLoad.Data.Storage.Providers;
using DamLoad.Data.Storage;
using Microsoft.Extensions.Configuration;

public class StorageFactory
{
    private readonly string _connectionString;
    private readonly IConfiguration _configuration;
    private readonly StorageRootResolver _rootResolver;

    public StorageFactory(string connectionString, IConfiguration configuration, StorageRootResolver rootResolver)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _rootResolver = rootResolver ?? throw new ArgumentNullException(nameof(rootResolver));
    }

    public IStorageProvider CreateStorageProvider()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
            throw new InvalidOperationException("Storage connection string is missing.");

        var storageType = DetectStorageType();

        return storageType switch
        {
            StorageType.Azure => new AzureStorageProvider(_connectionString, _configuration, _rootResolver),
            StorageType.Aws => throw new NotImplementedException("AWS S3 provider not yet implemented."),
            _ => throw new InvalidOperationException("Unsupported or unknown storage provider.")
        };
    }

    private StorageType DetectStorageType()
    {
        if (_connectionString.Contains("DefaultEndpointsProtocol=", StringComparison.OrdinalIgnoreCase) &&
            _connectionString.Contains("AccountName=", StringComparison.OrdinalIgnoreCase))
            return StorageType.Azure;

        if (_connectionString.Contains("AWSAccessKeyId=", StringComparison.OrdinalIgnoreCase) &&
            _connectionString.Contains("AWSSecretKey=", StringComparison.OrdinalIgnoreCase))
            return StorageType.Aws;

        return StorageType.Unknown;
    }

    private enum StorageType
    {
        Unknown,
        Azure,
        Aws
    }
}
