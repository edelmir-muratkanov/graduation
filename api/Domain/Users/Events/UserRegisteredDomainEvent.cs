namespace Domain.Users.Events;

public sealed class UserRegisteredDomainEvent(User user) : IDomainEvent
{
    public User User { get; set; } = user;
}
