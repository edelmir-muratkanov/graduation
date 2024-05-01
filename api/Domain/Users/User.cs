using Domain.Users.Events;

namespace Domain.Users;

public class User : Entity
{
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

    public string Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public string? Token { get; private set; }

    public static Result<User> Create(string email, string password, Role role = Role.User)
    {
        var user = new User(Guid.NewGuid(), email, password, role);

        user.Raise(new UserRegisteredDomainEvent(user));
        return user;
    }

    public Result UpdateToken(string token)
    {
        Token = token;

        return Result.Success();
    }
}
