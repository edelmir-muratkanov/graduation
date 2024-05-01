using Domain.Projects;

namespace Application.Project.Remove;

internal sealed class RemoveProjectCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectCommand>
{
    public async Task<Result> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        Result isOwnerResult = project.IsOwner(currentUserService.Id!);
        if (isOwnerResult.IsFailure)
        {
            return isOwnerResult;
        }

        projectRepository.Remove(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
