using Application.Calculations;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;
using Domain.Properties;

namespace Application.Project.EventHandlers;

internal sealed class ProjectParameterRemovedDomainEventHandler(
    ICalculationRepository calculationRepository,
    IPropertyRepository propertyRepository,
    IMethodRepository methodRepository,
    ICalculationService calculationService)
    : INotificationHandler<ProjectParameterRemovedDomainEvent>
{
    public async Task Handle(ProjectParameterRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Project.Methods.Count == 0)
        {
            return;
        }

        foreach (ProjectMethod projectMethod in notification.Project.Methods)
        {
            Domain.Properties.Property? property = await propertyRepository
                .GetByIdAsync(notification.ProjectParameter.Id, cancellationToken);

            Domain.Methods.Method? method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);

            MethodParameter? methodParameter = method!.Parameters
                .FirstOrDefault(p => p.PropertyId == notification.ProjectParameter.PropertyId);

            if (methodParameter is null)
            {
                continue;
            }

            Calculation? calculation = await calculationRepository.GetByProjectAndMethodAsync(
                projectMethod.ProjectId,
                projectMethod.MethodId,
                cancellationToken);

            if (calculation is null)
            {
                throw new CalculationNotFoundException(projectMethod.ProjectId, projectMethod.MethodId);
            }

            Belonging belonging = calculationService.CalculateBelongingDegree(
                null,
                methodParameter.FirstParameters,
                methodParameter.SecondParameters);

            calculation.RemoveItem(property!.Name);
            calculation.AddItem(property.Name, belonging);
        }
    }
}
