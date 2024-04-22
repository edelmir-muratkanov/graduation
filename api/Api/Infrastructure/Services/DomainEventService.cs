using Api.Shared;
using Api.Shared.Interfaces;
using Api.Shared.Models;
using MediatR;

namespace Api.Infrastructure.Services;

public class DomainEventService(ILogger<DomainEventService> logger, IPublisher publisher) : IDomainEventService
{
    public Task Publish(DomainEvent domainEvent)
    {
        logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
        return publisher.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}