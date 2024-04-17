using Domain.Primitives;
using Newtonsoft.Json;
using Persistence.Outbox;

namespace Persistence;

internal sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}