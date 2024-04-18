using Api.Shared;
using Api.Shared.Mappings;

namespace Api.Domain.Users;

public enum Role
{
    User,
    Admin
}

public class User : IHasDomainEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
    public List<DomainEvent> DomainEvents { get; } = [];
}

public class UserRegisteredDomainEvent(User user) : DomainEvent
{
    public User User { get; } = user;
}

public class UserRecord : IMapFrom<User>
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}