using Domain.Projects;

namespace Application.Project.AddMembers;

/// <summary>
/// Обработчик команды <see cref="AddProjectMembersCommand"/>.
/// </summary>
internal sealed class AddProjectMembersCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IProjectMemberRepository projectMemberRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProjectMembersCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(AddProjectMembersCommand request, CancellationToken cancellationToken)
    {
        // Получение проекта по его идентификатору
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        // Проверка прав текущего пользователя на добавление участников
        Result isOwnerResult = project.IsOwner(currentUserService.Id!);
        if (isOwnerResult.IsFailure)
        {
            return isOwnerResult;
        }

        // Добавление участников в проект
        var results = request.MemberIds.Select(memberId => project.AddMember(memberId)).ToList();
        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        // Вставка участников проекта в репозиторий и сохранение изменений
        projectMemberRepository.InsertRange(results.Select(r => r.Value).ToList());
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
