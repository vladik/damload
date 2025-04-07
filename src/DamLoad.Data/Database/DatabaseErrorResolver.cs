using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data.Common;

namespace DamLoad.Data.Database
{
    public static class DatabaseErrorResolver
    {
        public static bool IsUniqueViolation(DbException? ex)
        {
            if (ex is null) return false;

            return ex switch
            {
                PostgresException pg when pg.SqlState == "23505" => true,
                SqlException sql when sql.Number is 2601 or 2627 => true,
                //MySqlException my when my.Number == 1062 => true,
                //SQLiteException sq when sq.Message.Contains("UNIQUE constraint failed") => true,
                _ => false
            };
        }
    }
}