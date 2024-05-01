using Application.Calculations;
using Application.Method;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;

namespace Application.Project.EventHandlers;

internal sealed class ProjectBaseInfoUpdatedDomainEventHandler(
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    IMethodRepository methodRepository)
    : INotificationHandler<ProjectBaseInfoUpdatedDomainEvent>
{
    public async Task Handle(ProjectBaseInfoUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository
            .GetByIdAsync(notification.ProjectId, cancellationToken);

        if (project is null)
        {
            throw new ProjectNotFoundException(notification.ProjectId);
        }

        foreach (ProjectMethod projectMethod in project.Methods)
        {
            Domain.Methods.Method method = await methodRepository.GetByIdAsync(
                projectMethod.MethodId,
                cancellationToken);

            if (method is null)
            {
                throw new MethodNotFoundException(projectMethod.MethodId);
            }

            Calculation? calculation = await calculationRepository.GetByProjectAndMethodAsync(
                projectMethod.ProjectId,
                projectMethod.MethodId,
                cancellationToken);

            if (calculation is null)
            {
                throw new CalculationNotFoundException(projectMethod.ProjectId, projectMethod.MethodId);
            }

            int degree = method.CollectorTypes.Contains(project.CollectorType) ? 1 : -1;


            calculation.RemoveItem("Тип коллектора");
            calculation.AddItem("Тип коллектора", new Belonging(degree));

            calculationRepository.Update(calculation);
        }
    }
}
