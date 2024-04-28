using Application.Calculations;
using Application.Method;
using Application.Property;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;
using Domain.Properties;

namespace Application.Project.EventHandlers;

internal sealed class ProjectParameterRemovedDomainEventHandler(
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    IPropertyRepository propertyRepository,
    IMethodRepository methodRepository,
    ICalculationService calculationService)
    : INotificationHandler<ProjectParameterRemovedDomainEvent>
{
    public async Task Handle(ProjectParameterRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository
            .GetByIdAsync(notification.ProjectId, cancellationToken);

        if (project is null)
        {
            throw new ProjectNotFoundException(notification.ProjectId);
        }

        if (project.Methods.Count == 0)
        {
            return;
        }

        foreach (ProjectMethod projectMethod in project.Methods)
        {
            Domain.Properties.Property? property = await propertyRepository
                .GetByIdAsync(notification.PropertyId, cancellationToken);

            if (property is null)
            {
                throw new PropertyNotFoundException(notification.PropertyId);
            }
            
            Domain.Methods.Method? method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);

            if (method is null)
            {
                throw new MethodNotFoundException(projectMethod.MethodId);
            }

            MethodParameter? methodParameter = method.Parameters
                .FirstOrDefault(p => p.PropertyId == notification.PropertyId);

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
