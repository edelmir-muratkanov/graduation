using Application.Abstractions.Authentication;
using Domain.Projects;

namespace Application.Project.RemoveParameter;

internal class RemoveProjectParameterCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IProjectParameterRepository projectParameterRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectParameterCommand>
{
    public async Task<Result> Handle(RemoveProjectParameterCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        var userId = currentUserService.Id ?? string.Empty;

        var isOwnerResult = project.IsOwner(userId);
        var isMemberResult = project.IsMember(userId);

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