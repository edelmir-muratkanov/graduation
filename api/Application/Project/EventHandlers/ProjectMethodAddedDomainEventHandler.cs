using Application.Method;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects.Events;
using Domain.Properties;

namespace Application.Project.EventHandlers;

internal sealed class ProjectMethodAddedDomainEventHandler(
    ICalculationRepository calculationRepository,
    IMethodRepository methodRepository,
    IPropertyRepository propertyRepository,
    ICalculationService calculationService) : INotificationHandler<ProjectMethodAddedDomainEvent>
{
    public async Task Handle(ProjectMethodAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository
            .GetByIdAsync(notification.ProjectMethod.MethodId, cancellationToken);

        if (method is null)
        {
            throw new MethodNotFoundException(notification.ProjectMethod.MethodId);
        }

        Calculation? calculation = await calculationRepository
            .GetByProjectAndMethodAsync(notification.Project.Id, method.Id, cancellationToken);

        if (calculation is not null)
        {
            return;
        }

        calculation = Calculation.Create(notification.Project.Id, method.Id);

        var collectorBelonging = new Belonging(method.CollectorTypes.Contains(notification.Project.CollectorType)
            ? 1
            : -1);

        calculation.AddItem("Тип коллектора", collectorBelonging);

        foreach (MethodParameter methodParameter in method.Parameters)
        {
            Domain.Properties.Property? property = await propertyRepository
                .GetByIdAsync(methodParameter.PropertyId, cancellationToken);

            double? projectParameterValue = notification.Project.Parameters
                .FirstOrDefault(p => p.PropertyId == methodParameter.PropertyId)?.Value;

            Belonging belonging = calculationService.CalculateBelongingDegree(
                projectParameterValue,
                methodParameter.FirstParameters,
                methodParameter.SecondParameters);

            calculation.AddItem(property!.Name, belonging);
        }

        calculationRepository.Insert(calculation);
    }
}
