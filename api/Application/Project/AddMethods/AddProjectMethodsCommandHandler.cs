using Application.Abstractions.Authentication;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.AddMethods;

internal sealed class AddProjectMethodsCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IMethodRepository methodRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProjectMethodsCommand>
{
    public async Task<Result> Handle(AddProjectMethodsCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null) return Result.Failure(ProjectErrors.NotFound);

        var isOwnerResult = project.IsOwner(currentUserService.Id!);
        var isMemberResult = project.IsMember(currentUserService.Id!);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        foreach (var methodId in request.MethodIds)
            if (!await methodRepository.Exists(methodId))
                return Result.Failure(MethodErrors.NotFound);

        var results = request.MethodIds.Select(methodId => project.AddMethod(methodId)).ToList();

        if (results.Any(r => r.IsFailure)) return Result.Failure(ValidationError.FromResults(results));

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}