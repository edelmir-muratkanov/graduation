using Domain.Methods;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal class MethodRepository(ApplicationWriteDbContext context) : IMethodRepository
{
    public async Task<Method?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Methods
            .Include(m => m.Parameters)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<bool> IsNameUniqueAsync(string name)
    {
        return !await context.Methods.AnyAsync(m => m.Name == name);
    }

    public void Insert(Method method)
    {
        context.Methods.Add(method);
    }

    public void Update(Method method)
    {
        context.Methods.Update(method);
    }

    public void Remove(Method method)
    {
        context.Methods.Remove(method);
    }
}