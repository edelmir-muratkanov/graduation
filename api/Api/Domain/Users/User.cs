using Api.Shared;
using Api.Shared.Mappings;
using Api.Shared.Models;

namespace Api.Domain.Users;

public enum Role
{
    User,
    Admin
}

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public string? Token { get; private set; }
    public List<IDomainEvent> DomainEvents { get; } = [];

    private User(Guid id, string email, string password, Role role)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
    }

    private User()
    {
    }

    public static Result<User> Create(string email, string password, Role role = Role.User)
    {
        var user = new User(Guid.NewGuid(), email, password, role);

        user.DomainEvents.Add(new UserRegisteredDomainEvent(user));
        return user;
    }

    public Result UpdateToken(string token)
    {
        Token = token;

        return Result.Success();
    }
}

public class UserRegisteredDomainEvent(User user) : IDomainEvent
{
    public User User { get; } = user;
}

public class UserRecord : IMapFrom<User>
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}