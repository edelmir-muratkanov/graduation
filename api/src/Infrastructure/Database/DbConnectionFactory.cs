using System.Data;
using Npgsql;

namespace Infrastructure.Database;

/// <summary>
/// Фабрика подключений к базе данных.
/// </summary>
/// <param name="dataSource">Источник данных PostgreSQL.</param>
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource)
{
    /// <summary>
    /// Открывает новое соединение с базой данных.
    /// </summary>
    /// <returns>Объект подключения к базе данных.</returns>
    public IDbConnection OpenConnection()
    {
        NpgsqlConnection? connection = dataSource.OpenConnection();
        return connection;
    }
}
