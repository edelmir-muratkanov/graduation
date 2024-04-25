using Application.Calculations;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;
using Domain.Properties;

namespace Application.Project.EventHandlers;

internal sealed class ProjectParameterAddedDomainEventHandler(
    ICalculationRepository calculationRepository,
    ICalculationService calculationService,
    IPropertyRepository propertyRepository,
    IMethodRepository methodRepository,
    ICalculationItemRepository calculationItemRepository)
    : INotificationHandler<ProjectParameterAddedDomainEvent>
{
    public async Task Handle(ProjectParameterAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Project.Methods.Count == 0)
        {
            return;
        }

        Domain.Properties.Property? property = await propertyRepository
            .GetByIdAsync(notification.ProjectParameter.PropertyId, cancellationToken);


        foreach (ProjectMethod projectMethod in notification.Project.Methods)
        {
            Domain.Methods.Method method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);

            MethodParameter? methodParameter = method!.Parameters
                .FirstOrDefault(p => p.PropertyId == notification.ProjectParameter.PropertyId);

            if (methodParameter is null)
            {
                continue;
            }

            Calculation? calculation = await calculationRepository
                .GetByProjectAndMethodAsync(notification.Project.Id, method!.Id, cancellationToken);

            if (calculation is null)
            {
                throw new CalculationNotFoundException(notification.Project.Id, method.Id);
            }

            Result<CalculationItem> deleteResults = calculation.RemoveItem(property!.Name);
            calculationItemRepository.Remove(deleteResults.Value);

            Belonging belonging = calculationService.CalculateBelongingDegree(
                notification.ProjectParameter.Value,
                methodParameter.FirstParameters,
                methodParameter.SecondParameters);

            Result<CalculationItem> addResult = calculation.AddItem(property.Name, belonging);

            calculationItemRepository.Insert(addResult.Value);
        }
    }
}
