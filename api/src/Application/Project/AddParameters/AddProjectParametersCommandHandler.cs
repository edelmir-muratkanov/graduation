using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;

namespace Application.Project.AddParameters;

/// <summary>
/// Обработчик команды <see cref="AddProjectParametersCommand"/>.
/// </summary>
internal class AddProjectParametersCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IPropertyRepository propertyRepository,
    IProjectParameterRepository projectParameterRepository,
    ICalculationService calculationService,
    IMethodRepository methodRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProjectParametersCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(AddProjectParametersCommand request, CancellationToken cancellationToken)
    {
        // Получение проекта по его идентификатору
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        // Получение идентификатора текущего пользователя
        string? userId = currentUserService.Id ?? string.Empty;

        // Проверка, является ли текущий пользователь владельцем или участником проекта
        Result? isOwnerResult = project.IsOwner(userId);
        Result? isMemberResult = project.IsMember(userId);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        // Проверка существования свойств по их идентификаторам
        foreach (AddProjectParameter? parameter in request.Parameters)
        {
            if (!await propertyRepository.Exists(parameter.PropertyId))
            {
                return Result.Failure(PropertyErrors.NotFound);
            }
        }


        // Добавление параметров к проекту
        var results = request.Parameters.Select(p =>
                project.AddParameter(p.PropertyId, p.Value))
            .ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        // Обновление расчетов, связанных с проектом
        foreach (ProjectMethod projectMethod in project.Methods)
        {
            Domain.Methods.Method? method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);
            
            Result result = await calculationService.Update(project, method!);
            if (result.IsFailure)
            {
                return Result.Failure(ValidationError.FromResults([result]));
            }
        }

        // Вставка параметров проекта в репозиторий
        projectParameterRepository.InsertRange(results.Select(r => r.Value).ToList());
        
        // Сохранение изменений
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
