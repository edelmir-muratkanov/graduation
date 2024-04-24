using Domain.Users;

namespace Infrastructure.Database.Models;

internal sealed class UserReadModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}
