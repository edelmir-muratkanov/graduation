using Application.Project;
using Application.Property;
using Domain.Calculation;
using Domain.Methods;
using Domain.Methods.Events;
using Domain.Projects;
using Domain.Properties;

namespace Application.Method.EventHandlers;

public class MethodParameterAddedDomainEventHandler(
    IMethodRepository methodRepository,
    ICalculationRepository calculationRepository,
    ICalculationItemRepository calculationItemRepository,
    ICalculationService calculationService,
    IProjectRepository projectRepository,
    IPropertyRepository propertyRepository) : INotificationHandler<MethodParameterAddedDomainEvent>
{
    public async Task Handle(MethodParameterAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(notification.MethodId, cancellationToken);

        if (method is null)
        {
            throw new MethodNotFoundException(notification.MethodId);
        }

        MethodParameter? methodParameter = method.Parameters
            .FirstOrDefault(p => p.Id == notification.MethodParameterId);

        Domain.Properties.Property? property = await propertyRepository
            .GetByIdAsync(methodParameter!.PropertyId, cancellationToken);

        if (property is null)
        {
            throw new PropertyNotFoundException(methodParameter.PropertyId);
        }

        List<Calculation> calculations = await calculationRepository.GetByMethodAsync(method.Id, cancellationToken);

        foreach (Calculation calculation in calculations)
        {
            Domain.Projects.Project project =
                await projectRepository.GetByIdAsync(calculation.ProjectId, cancellationToken);
            if (project is null)
            {
                throw new ProjectNotFoundException(calculation.ProjectId);
            }

            ProjectParameter? projectParameter = project.Parameters
                .FirstOrDefault(p => p.PropertyId == methodParameter.PropertyId);

            Belonging belonging = calculationService.CalculateBelongingDegree(
                projectParameter?.Value,
                methodParameter.FirstParameters,
                methodParameter.SecondParameters);

            Result<CalculationItem> calculationItem = calculation.AddItem(property.Name, belonging);

            calculationItemRepository.Insert(calculationItem.Value);
        }
    }
}
