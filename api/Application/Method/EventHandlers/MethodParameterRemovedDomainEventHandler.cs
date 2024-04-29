using Application.Property;
using Domain.Calculation;
using Domain.Methods;
using Domain.Methods.Events;
using Domain.Properties;

namespace Application.Method.EventHandlers;

public class MethodParameterRemovedDomainEventHandler(
    IMethodRepository methodRepository,
    IPropertyRepository propertyRepository,
    ICalculationRepository calculationRepository)
    : INotificationHandler<MethodParameterRemovedDomainEvent>
{
    public async Task Handle(MethodParameterRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(notification.MethodId, cancellationToken);

        if (method is null)
        {
            throw new MethodNotFoundException(notification.MethodId);
        }

        Domain.Properties.Property? property = await propertyRepository
            .GetByIdAsync(notification.PropertyId, cancellationToken);

        if (property is null)
        {
            throw new PropertyNotFoundException(notification.PropertyId);
        }

        List<Calculation> calculations = await calculationRepository
            .GetByMethodAsync(method.Id, cancellationToken);

        foreach (Calculation calculation in calculations)
        {
            calculation.RemoveItem(property.Name);
            calculationRepository.Update(calculation);
        }
    }
}
