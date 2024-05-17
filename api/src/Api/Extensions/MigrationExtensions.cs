using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

/// <summary>
/// Расширение для применения миграций базы данных при запуске приложения.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Применяет все ожидающие миграции для контекста базы данных приложения.
    /// </summary>
    /// <param name="app">Экземпляр <see cref="IApplicationBuilder"/>.</param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        // Создаем область служб для обеспечения правильного управления жизненным циклом сервисов.
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        // Получаем экземпляр контекста базы данных записи приложения.
        using ApplicationWriteDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationWriteDbContext>();

        // Применяем все ожидающие миграции для базы данных.
        dbContext.Database.Migrate();
    }
}
