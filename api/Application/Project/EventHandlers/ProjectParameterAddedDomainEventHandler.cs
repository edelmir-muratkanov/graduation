using Application.Calculations;
using Application.Method;
using Application.Property;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;
using Domain.Properties;

namespace Application.Project.EventHandlers;

internal sealed class ProjectParameterAddedDomainEventHandler(
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    ICalculationService calculationService,
    IPropertyRepository propertyRepository,
    IMethodRepository methodRepository,
    ICalculationItemRepository calculationItemRepository)
    : INotificationHandler<ProjectParameterAddedDomainEvent>
{
    public async Task Handle(ProjectParameterAddedDomainEvent notification, CancellationToken cancellationToken)
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

        ProjectParameter projectParameter = project.Parameters
            .FirstOrDefault(p => p.Id == notification.ProjectParameterId);

        Domain.Properties.Property? property = await propertyRepository
            .GetByIdAsync(projectParameter!.PropertyId, cancellationToken);

        if (property is null)
        {
            throw new PropertyNotFoundException(projectParameter.PropertyId);
        }


        foreach (ProjectMethod projectMethod in project.Methods)
        {
            Domain.Methods.Method method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);

            if (method is null)
            {
                throw new MethodNotFoundException(projectMethod.MethodId);
            }

            MethodParameter? methodParameter = method.Parameters
                .FirstOrDefault(p => p.PropertyId == property.Id);

            if (methodParameter is null)
            {
                continue;
            }

            Calculation? calculation = await calculationRepository
                .GetByProjectAndMethodAsync(project.Id, method.Id, cancellationToken);

            if (calculation is null)
            {
                throw new CalculationNotFoundException(project.Id, method.Id);
            }

            CalculationItem? calculationItem = calculation.Items.FirstOrDefault(i => i.PropertyName == property.Name);

            if (calculationItem is null)
            {
                continue;
            }

            Belonging belonging = calculationService.CalculateBelongingDegree(
                projectParameter.Value,
                methodParameter.FirstParameters,
                methodParameter.SecondParameters);

            calculationItem.UpdateBelonging(belonging);

            calculationRepository.Update(calculation);
        }
    }
}
