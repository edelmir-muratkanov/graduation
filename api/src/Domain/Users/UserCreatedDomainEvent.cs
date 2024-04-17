using Domain.Primitives;

namespace Domain.Users;

public record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;