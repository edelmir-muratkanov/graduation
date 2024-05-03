using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.AddMethods;

internal sealed class AddProjectMethodsCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IMethodRepository methodRepository,
    IProjectMethodRepository projectMethodRepository,
    ICalculationService calculationService,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProjectMethodsCommand>
{
    public async Task<Result> Handle(AddProjectMethodsCommand request, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        Result? isOwnerResult = project.IsOwner(currentUserService.Id!);
        Result? isMemberResult = project.IsMember(currentUserService.Id!);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        foreach (Guid methodId in request.MethodIds)
        {
            if (!await methodRepository.Exists(methodId))
            {
                return Result.Failure(MethodErrors.NotFound);
            }
        }

        var results = request.MethodIds.Select(methodId => project.AddMethod(methodId)).ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        foreach (Guid methodId in request.MethodIds)
        {
            Domain.Methods.Method method = await methodRepository.GetByIdAsync(methodId, cancellationToken);
            Result result = await calculationService.Create(project, method!);
            if (result.IsFailure)
            {
                return Result.Failure(ValidationError.FromResults([result]));
            }
        }

        projectMethodRepository.InsertRange(results.Select(r => r.Value).ToList());
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
