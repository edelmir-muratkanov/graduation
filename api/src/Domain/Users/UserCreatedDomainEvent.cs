using Domain.Primitives;

namespace Domain.Users;

public record UserCreatedDomainEvent(Guid Id, Guid UserId) : DomainEvent(Id);