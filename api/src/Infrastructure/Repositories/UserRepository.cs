using Domain.Users;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с пользователями.
/// </summary>
internal sealed class UserRepository(ApplicationWriteDbContext context) : IUserRepository
{
    /// <inheritdoc />
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await context.Users.AnyAsync(u => u.Email == email);
    }

    /// <inheritdoc />
    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
