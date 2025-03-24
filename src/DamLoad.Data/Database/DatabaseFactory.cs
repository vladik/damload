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
            throw new InvalidOperationException("Database connection string is missing.");

        return GetDbType() switch
        {
            DbType.MsSql => new SqlConnection(_connectionString),
            DbType.PostgreSql => new NpgsqlConnection(_connectionString),
            _ => throw new InvalidOperationException("Unsupported database type.")
        };
    }

    public string GetDbUtcNow()
    {
        return GetDbType() switch
        {
            DbType.MsSql => "GETUTCDATE()",
            DbType.PostgreSql => "NOW() AT TIME ZONE 'UTC'",
            _ => throw new InvalidOperationException("Unsupported database type.")
        };
    }

    private DbType GetDbType()
    {
        if (_connectionString.Contains("Server=", StringComparison.OrdinalIgnoreCase) ||
            _connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase))
            return DbType.MsSql;

        if (_connectionString.Contains("Host=", StringComparison.OrdinalIgnoreCase))
            return DbType.PostgreSql;

        return DbType.Unknown;
    }

    private enum DbType
    {
        Unknown,
        MsSql,
        PostgreSql
    }
}
