using Application.Abstractions.Authentication;
using Domain.Projects;

namespace Application.Project.RemoveMethod;

internal sealed class RemoveProjectMethodCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectMethodCommand>
{
    public async Task<Result> Handle(RemoveProjectMethodCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null) return Result.Failure(ProjectErrors.NotFound);

        var isOwnerResult = project.IsOwner(currentUserService.Id!);
        var isMemberResult = project.IsMember(currentUserService.Id!);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        project.RemoveMethod(request.MethodId);

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}