using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.RemoveParameter;

/// <summary>
/// Обработчик команды <see cref="RemoveProjectParameterCommand"/>
/// </summary>
internal class RemoveProjectParameterCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IMethodRepository methodRepository,
    ICalculationService calculationService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectParameterCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(RemoveProjectParameterCommand request, CancellationToken cancellationToken)
    {
        // Получение проекта по его идентификатору
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        // Получение идентификатора текущего пользователя
        string userId = currentUserService.Id ?? string.Empty;

        // Проверка, является ли текущий пользователь владельцем или участником проекта
        Result isOwnerResult = project.IsOwner(userId);
        Result isMemberResult = project.IsMember(userId);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        // Удаление параметра из проекта
        project.RemoveParameter(request.ParameterId);

        // Обновление всех связанных расчетов
        foreach (ProjectMethod projectMethod in project.Methods)
        {
            Domain.Methods.Method? method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);
            
            Result result = await calculationService.Update(project, method!);
            if (result.IsFailure)
            {
                return result;
            }
        }

        // Сохранение изменений
        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
