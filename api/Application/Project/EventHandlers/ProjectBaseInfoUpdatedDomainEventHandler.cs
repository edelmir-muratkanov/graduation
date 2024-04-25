using Application.Calculations;
using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Projects.Events;

namespace Application.Project.EventHandlers;

internal sealed class ProjectBaseInfoUpdatedDomainEventHandler(
    ICalculationRepository calculationRepository,
    IMethodRepository methodRepository)
    : INotificationHandler<ProjectBaseInfoUpdatedDomainEvent>
{
    public async Task Handle(ProjectBaseInfoUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (ProjectMethod projectMethod in notification.Project.Methods)
        {
            Domain.Methods.Method method = await methodRepository.GetByIdAsync(
                projectMethod.MethodId,
                cancellationToken);

            Calculation? calculation = await calculationRepository.GetByProjectAndMethodAsync(
                projectMethod.ProjectId,
                projectMethod.MethodId,
                cancellationToken);

            if (calculation is null)
            {
                throw new CalculationNotFoundException(projectMethod.ProjectId, projectMethod.MethodId);
            }

            int degree = method!.CollectorTypes.Contains(notification.Project.CollectorType) ? 1 : -1;


            calculation.RemoveItem("Тип коллектора");
            calculation.AddItem("Тип коллектора", new Belonging(degree));

            calculationRepository.Update(calculation);
        }
    }
}
