using Domain.Methods;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с методами.
/// </summary>
internal sealed class MethodRepository(ApplicationWriteDbContext context) : IMethodRepository
{
    /// <inheritdoc />
    public async Task<Method?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Methods
            .Include(m => m.Parameters)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> IsNameUniqueAsync(string name)
    {
        return !await context.Methods.AnyAsync(m => m.Name == name);
    }

    /// <inheritdoc />
    public async Task<bool> Exists(Guid id)
    {
        return await context.Methods.AnyAsync(m => m.Id == id);
    }

    /// <inheritdoc />
    public void Insert(Method method)
    {
        context.Methods.Add(method);
    }

    /// <inheritdoc />
    public void Update(Method method)
    {
        context.Methods.Update(method);
    }

    /// <inheritdoc />
    public void Remove(Method method)
    {
        context.Methods.Remove(method);
    }
}
