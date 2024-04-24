using Domain.Projects;
using Domain.Properties;

namespace Application.Project.Create;

internal class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    IProjectMethodRepository projectMethodRepository,
    IProjectParameterRepository projectParameterRepository,
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProjectCommand>
{
    public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        foreach (var parameter in request.Parameters)
            if (!await propertyRepository.Exists(parameter.PropertyId))
                return Result.Failure(ProjectParameterErrors.InvalidProperty);

        var projectResult = Domain.Projects.Project.Create(
            request.Name,
            request.Country,
            request.Operator,
            request.ProjectType,
            request.CollectorType);

        if (projectResult.IsFailure)
            return projectResult;

        var project = projectResult.Value;

        var parameterResults = request.Parameters
            .Select(p => project.AddParameter(p.PropertyId, p.Value))
            .ToList();

        if (parameterResults.Any(r => r.IsFailure))
            return Result.Failure(ValidationError.FromResults(parameterResults));

        var methodResults = request.MethodIds
            .Select(m => project.AddMethod(m))
            .ToList();

        if (methodResults.Any(r => r.IsFailure))
            return Result.Failure(ValidationError.FromResults(methodResults));

        projectRepository.Insert(project);
        projectMethodRepository.InsertRange(project.Methods);
        projectParameterRepository.InsertRange(project.Parameters);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}