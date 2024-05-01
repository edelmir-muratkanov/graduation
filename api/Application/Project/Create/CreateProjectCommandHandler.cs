using Domain.Projects;

namespace Application.Project.Create;

internal class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProjectCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        Result<Domain.Projects.Project>? projectResult = Domain.Projects.Project.Create(
            request.Name,
            request.Country,
            request.Operator,
            request.ProjectType,
            request.CollectorType);

        if (projectResult.IsFailure)
        {
            return Result.Failure<Guid>(projectResult.Error);
        }

        projectRepository.Insert(projectResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(projectResult.Value.Id);
    }
}
