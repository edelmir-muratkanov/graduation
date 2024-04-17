using Domain.Primitives;

namespace Domain.Users;

public sealed class User : AggregateRoot
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }

    private User()
    {
    }

    private User(Guid id, Email email, Password password, Role role) : base(id)
    {
        Email = email;
        Password = password;
        Role = role;
    }


    public static User Create(Email email, Password password, Role role = Role.User)
    {
        var user = new User(Guid.NewGuid(), email, password, role);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
}