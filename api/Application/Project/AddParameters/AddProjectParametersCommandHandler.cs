using Application.Abstractions.Authentication;
using Domain.Projects;
using Domain.Properties;

namespace Application.Project.AddParameters;

internal class AddProjectParametersCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IPropertyRepository propertyRepository,
    IProjectParameterRepository projectParameterRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProjectParametersCommand>
{
    public async Task<Result> Handle(AddProjectParametersCommand request, CancellationToken cancellationToken)
    {
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        string? userId = currentUserService.Id ?? string.Empty;

        Result? isOwnerResult = project.IsOwner(userId);
        Result? isMemberResult = project.IsMember(userId);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        foreach (AddProjectParameter? parameter in request.Parameters)
        {
            if (!await propertyRepository.Exists(parameter.PropertyId))
            {
                return Result.Failure(PropertyErrors.NotFound);
            }
        }


        var results = request.Parameters.Select(p =>
                project.AddParameter(p.PropertyId, p.Value))
            .ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        projectParameterRepository.InsertRange(project.Parameters);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
