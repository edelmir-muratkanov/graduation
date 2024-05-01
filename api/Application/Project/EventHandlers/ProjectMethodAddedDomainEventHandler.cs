using Application.Method;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;
using Domain.Properties;

namespace Application.Project.EventHandlers;

internal sealed class ProjectMethodAddedDomainEventHandler(
    ICalculationRepository calculationRepository,
    IMethodRepository methodRepository,
    IPropertyRepository propertyRepository,
    IProjectRepository projectRepository,
    ICalculationService calculationService) : INotificationHandler<ProjectMethodAddedDomainEvent>
{
    public async Task Handle(ProjectMethodAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository
            .GetByIdAsync(notification.ProjectId, cancellationToken);

        if (project is null)
        {
            throw new ProjectNotFoundException(notification.ProjectId);
        }

        Domain.Methods.Method? method = await methodRepository
            .GetByIdAsync(notification.MethodId, cancellationToken);

        if (method is null)
        {
            throw new MethodNotFoundException(notification.MethodId);
        }


        Calculation? calculation = await calculationRepository
            .GetByProjectAndMethodAsync(notification.ProjectId, method.Id, cancellationToken);

        if (calculation is not null)
        {
            return;
        }

        calculation = Calculation.Create(notification.ProjectId, method.Id);

        var collectorBelonging = new Belonging(method.CollectorTypes.Contains(project.CollectorType)
            ? 1
            : -1);

        calculation.AddItem("Тип коллектора", collectorBelonging);

        foreach (MethodParameter methodParameter in method.Parameters)
        {
            Domain.Properties.Property? property = await propertyRepository
                .GetByIdAsync(methodParameter.PropertyId, cancellationToken);

            double? projectParameterValue = project.Parameters
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
