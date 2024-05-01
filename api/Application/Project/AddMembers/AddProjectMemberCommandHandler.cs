﻿using Domain.Projects;

namespace Application.Project.AddMembers;

internal sealed class AddProjectMembersCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IProjectMemberRepository projectMemberRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProjectMembersCommand>
{
    public async Task<Result> Handle(AddProjectMembersCommand request, CancellationToken cancellationToken)
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

        var results = request.MemberIds.Select(memberId => project.AddMember(memberId)).ToList();
        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        projectMemberRepository.InsertRange(results.Select(r => r.Value).ToList());
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
