using Application.Abstractions.Authentication;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared;

namespace Infrastructure.Database;

internal sealed class TrackAuditableEntityInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
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
                default:
                    break;
            }
        }
    }
}
