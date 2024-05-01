using Application.Project;
using Domain.Calculation;
using Domain.Methods;
using Domain.Methods.Events;
using Domain.Projects;

namespace Application.Method.EventHandlers;

internal sealed class MethodUpdatedDomainEventHandler(
    IMethodRepository methodRepository,
    ICalculationRepository calculationRepository,
    IProjectRepository projectRepository)
    : INotificationHandler<MethodUpdatedDomainEvent>
{
    public async Task Handle(MethodUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(notification.MethodId, cancellationToken);

        if (method is null)
        {
            throw new MethodNotFoundException(notification.MethodId);
        }

        List<Calculation> calculations = await calculationRepository.GetByMethodAsync(method.Id, cancellationToken);

        foreach (Calculation calculation in calculations)
        {
            Domain.Projects.Project? project = await projectRepository
                .GetByIdAsync(calculation.ProjectId, cancellationToken);

            if (project is null)
            {
                throw new ProjectNotFoundException(calculation.ProjectId);
            }

            var belonging = new Belonging(method.CollectorTypes.Contains(project.CollectorType) ? 1 : -1);

            calculation.UpdateItem("Тип коллектора", belonging);
            calculationRepository.Update(calculation);
        }
    }
}
