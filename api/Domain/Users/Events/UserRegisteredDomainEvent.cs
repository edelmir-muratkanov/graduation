namespace Domain.Users.Events;

public class UserRegisteredDomainEvent(User user) : IDomainEvent
{
    public User User { get; } = user;
}