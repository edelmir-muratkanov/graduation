using Domain.Projects;

namespace Application.Project.RemoveMember;

/// <summary>
/// Обработчик команды <see cref="RemoveProjectMemberCommand"/>.
/// </summary>
internal sealed class RemoveProjectMemberCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveProjectMemberCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(RemoveProjectMemberCommand request, CancellationToken cancellationToken)
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

        // Удаление участника из проекта
        project.RemoveMember(request.MemberId);

        // Обновление проекта в репозитории
        projectRepository.Update(project);
        // Сохранение изменений
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
