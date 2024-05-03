using Domain.Calculation;
using Domain.Projects;

namespace Application.Project.RemoveMethod;

internal sealed class RemoveProjectMethodCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProjectMethodCommand>
{
    public async Task<Result> Handle(RemoveProjectMethodCommand request, CancellationToken cancellationToken)
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

        project.RemoveMethod(request.MethodId);


        Calculation? calculation = await calculationRepository.GetOne(
            c => c.ProjectId == request.ProjectId && c.MethodId == request.MethodId,
            cancellationToken);

        calculationRepository.Remove(calculation!);

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
