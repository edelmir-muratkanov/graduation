using Application.Method;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;
using Domain.Properties;

namespace Application.Project.EventHandlers;

internal sealed class ProjectCreatedDomainEventHandler(
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    IMethodRepository methodRepository,
    IPropertyRepository propertyRepository,
    ICalculationService calculationService)
    : INotificationHandler<ProjectCreatedDomainEvent>
{
    public async Task Handle(ProjectCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project =
            await projectRepository.GetByIdAsync(notification.ProjectId, cancellationToken);

        if (project is null)
        {
            throw new ProjectNotFoundException(notification.ProjectId);
        }

        if (project.Methods.Count != 0)
        {
            List<Calculation> calculations = [];

            foreach (ProjectMethod projectMethod in project.Methods)
            {
                Domain.Methods.Method? method = await methodRepository
                    .GetByIdAsync(projectMethod.MethodId, cancellationToken);

                if (method is null)
                {
                    throw new MethodNotFoundException(projectMethod.MethodId);
                }

                var calculation = Calculation.Create(project.Id, method.Id);

                var collectorBelonging = new Belonging(method.CollectorTypes
                    .Contains(project.CollectorType)
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

                calculations.Add(calculation);
            }

            calculationRepository.InsertRange(calculations);
        }
    }
}
