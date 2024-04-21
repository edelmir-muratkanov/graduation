using Api.Domain.Properties;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Database.Repositories;

internal class PropertyRepository(ApplicationDbContext context) : IPropertyRepository
{
    public async Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Properties.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<bool> IsNameUniqueAsync(string name) =>
        !await context.Properties.AnyAsync(p => p.Name == name);

    public async Task<bool> Exists(Guid id) =>
        await context.Properties.AnyAsync(p => p.Id == id);

    public void Insert(Property property) => context.Properties.Add(property);
    public void Remove(Property property) => context.Properties.Remove(property);
    public void Update(Property property) => context.Properties.Update(property);
}