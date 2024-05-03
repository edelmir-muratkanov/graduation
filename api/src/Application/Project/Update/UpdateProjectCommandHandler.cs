using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.Update;

internal sealed class UpdateProjectCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IMethodRepository methodRepository,
    ICalculationService calculationService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProjectCommand>
{
    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        string userId = currentUserService.Id ?? string.Empty;

        Result isOwnerResult = project.IsOwner(userId);

        if (isOwnerResult.IsFailure)
        {
            return isOwnerResult;
        }

        project.UpdateBaseInfo(
            request.Name,
            request.Country,
            request.Operator,
            request.ProjectType,
            request.CollectorType);

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
