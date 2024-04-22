using Api.Domain.Methods;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Database.Repositories;

internal class MethodRepository(ApplicationDbContext context) : IMethodRepository
{
    public async Task<Method?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Methods.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<bool> IsNameUniqueAsync(string name)
    {
        return !await context.Methods.AnyAsync(m => m.Name == name);
    }

    public void Insert(Method method)
    {
        context.Methods.Add(method);
    }
}