using Domain.Projects;

namespace Application.Project.RemoveMember;

internal sealed class RemoveProjectMemberCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveProjectMemberCommand>
{
    public async Task<Result> Handle(RemoveProjectMemberCommand request, CancellationToken cancellationToken)
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

        project.RemoveMember(request.MemberId);

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
