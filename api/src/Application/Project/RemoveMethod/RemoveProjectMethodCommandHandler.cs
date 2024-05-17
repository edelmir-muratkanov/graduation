using Domain.Calculation;
using Domain.Projects;

namespace Application.Project.RemoveMethod;

/// <summary>
/// Обработчик команды <see cref="RemoveProjectMethodCommand"/>
/// </summary>
internal sealed class RemoveProjectMethodCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectMethodCommand>
{
    public async Task<Result> Handle(RemoveProjectMethodCommand request, CancellationToken cancellationToken)
    {
        // Получение проекта по его идентификатору
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        // Проверка, является ли текущий пользователь владельцем или участником проекта
        Result? isOwnerResult = project.IsOwner(currentUserService.Id!);
        Result? isMemberResult = project.IsMember(currentUserService.Id!);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        // Удаление метода из проекта
        project.RemoveMethod(request.MethodId);

        // Получение вычислений, связанных с удаляемым методом в проекте
        Calculation? calculation = await calculationRepository.GetOne(
            c => c.ProjectId == request.ProjectId && c.MethodId == request.MethodId,
            cancellationToken);

        // Удаление связанных вычислений
        calculationRepository.Remove(calculation!);

        // Обновление проекта в репозитории
        projectRepository.Update(project);
        
        // Сохранение изменений в базе данных
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
