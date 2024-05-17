using Application.Abstractions.Authentication;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Database;

/// <summary>
/// Перехватчик для отслеживания аудиторских сущностей при сохранении изменений в базе данных.
/// </summary>
/// <param name="currentUserService">Сервис для получения текущего пользователя.</param>
internal sealed class TrackAuditableEntityInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
    /// <inheritdoc/>
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
        {
            TrackEntityAsync(eventData.Context);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// Отслеживает изменения в аудиторских сущностях.
    /// </summary>
    /// <param name="context">Контекст базы данных Entity Framework.</param>
    private void TrackEntityAsync(DbContext context)
    {
        foreach (EntityEntry<AuditableEntity>? entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = currentUserService.Id;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = currentUserService.Id;
                    break;
                case EntityState.Deleted:
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
            }
        }
    }
}
