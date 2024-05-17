using Domain.Properties;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с свойствами.
/// </summary>
internal sealed class PropertyRepository(ApplicationWriteDbContext context) : IPropertyRepository
{
    /// <inheritdoc />
    public async Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Properties.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> IsNameUniqueAsync(string name)
    {
        return !await context.Properties.AnyAsync(p => p.Name == name);
    }

    /// <inheritdoc />
    public async Task<bool> Exists(Guid id)
    {
        return await context.Properties.AnyAsync(p => p.Id == id);
    }

    /// <inheritdoc />
    public void Insert(Property property)
    {
        context.Properties.Add(property);
    }

    /// <inheritdoc />
    public void Remove(Property property)
    {
        context.Properties.Remove(property);
    }

    /// <inheritdoc />
    public void Update(Property property)
    {
        context.Properties.Update(property);
    }
}
