using Domain.Users;

namespace Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationWriteDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await context.Users.AnyAsync(u => u.Email == email);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
