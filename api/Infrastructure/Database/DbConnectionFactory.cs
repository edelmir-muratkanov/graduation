using System.Data;
using Npgsql;

namespace Infrastructure.Database;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource)
{
    public IDbConnection OpenConnection()
    {
        var connection = dataSource.OpenConnection();
        return connection;
    }
}