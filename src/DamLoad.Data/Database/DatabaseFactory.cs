using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace DamLoad.Data.Database;
public class DatabaseFactory
{
    private readonly string _connectionString;

    public DatabaseFactory(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public IDbConnection CreateConnection()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Database connection string is missing.");
        }

        if (_connectionString.Contains("Server=", StringComparison.OrdinalIgnoreCase) ||
            _connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase))
        {
            return new SqlConnection(_connectionString); // MSSQL
        }

        if (_connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase))
        {
            return new NpgsqlConnection(_connectionString); // PostgreSQL
        }

        throw new InvalidOperationException("Unsupported database type. Connection string format not recognized.");
    }
    public string GetDbUtcNow()
    {
        return _connectionString.Contains("Server=", StringComparison.OrdinalIgnoreCase) ? "GETUTCDATE()" // MSSQL
             : _connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase) ? "NOW() AT TIME ZONE 'UTC'" // PostgreSQL
             : throw new InvalidOperationException("Unsupported database type.");
    }
}