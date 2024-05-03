using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.RemoveParameter;

internal class RemoveProjectParameterCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IMethodRepository methodRepository,
    ICalculationService calculationService,
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

        foreach (ProjectMethod projectMethod in project.Methods)
        {
            Domain.Methods.Method? method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);
            
            Result result = await calculationService.Update(project, method!);
            if (result.IsFailure)
            {
                return result;
            }
        }

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
