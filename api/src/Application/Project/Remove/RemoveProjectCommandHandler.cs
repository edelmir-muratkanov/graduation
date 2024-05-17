using Domain.Projects;

namespace Application.Project.Remove;

/// <summary>
/// Обработчик команды <see cref="RemoveProjectCommand"/>.
/// </summary>
internal sealed class RemoveProjectCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        // Получение проекта по его идентификатору
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        // Проверка, является ли текущий пользователь владельцем проекта
        Result isOwnerResult = project.IsOwner(currentUserService.Id!);
        if (isOwnerResult.IsFailure)
        {
            return isOwnerResult;
        }

        // Удаление проекта и сохранение изменений в БД
        projectRepository.Remove(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
