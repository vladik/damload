using DamLoad.Core.Configurations;

namespace DamLoad.Data.Storage
{
    public class StorageFactory
    {
        private readonly string _connectionString;
        private readonly ConfigurationService _configuration;

        public StorageFactory(string connectionString, ConfigurationService configurationService)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _configuration = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public IStorageProvider CreateStorageProvider()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("Storage connection string is missing.");
            }

            if (IsAzureStorage(_connectionString))
            {
                Console.WriteLine("Detected Azure Blob Storage as the storage provider.");
                return new AzureStorageProvider(_connectionString, _configuration);
            }

            if (IsAwsS3(_connectionString))
            {
                Console.WriteLine("Detected AWS S3 as the storage provider.");
                //return new AwsS3StorageProvider(_connectionString, _configuration);
            }

            throw new InvalidOperationException("Unsupported storage provider. Connection string format not recognized.");
        }

        private bool IsAzureStorage(string connectionString)
        {
            return connectionString.Contains("DefaultEndpointsProtocol=", StringComparison.OrdinalIgnoreCase) &&
                   connectionString.Contains("AccountName=", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsAwsS3(string connectionString)
        {
            return connectionString.Contains("AWSAccessKeyId=", StringComparison.OrdinalIgnoreCase) &&
                   connectionString.Contains("AWSSecretKey=", StringComparison.OrdinalIgnoreCase);
        }
    }
}
