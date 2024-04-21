using Api.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Database.Repositories;

internal class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(string email) =>
        !await context.Users.AnyAsync(u => u.Email == email);

    public void Insert(User user) =>
        context.Users.Add(user);
}