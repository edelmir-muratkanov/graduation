using Domain.Projects;

namespace Application.Project.RemoveParameter;

internal class RemoveProjectParameterCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectParameterCommand>
{
    public async Task<Result> Handle(RemoveProjectParameterCommand request, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        string userId = currentUserService.Id ?? string.Empty;

        Result isOwnerResult = project.IsOwner(userId);
        Result isMemberResult = project.IsMember(userId);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        project.RemoveParameter(request.ParameterId);

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
