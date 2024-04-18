namespace Api.Shared.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}