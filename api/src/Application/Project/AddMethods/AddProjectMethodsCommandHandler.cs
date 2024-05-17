using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.AddMethods;

/// <summary>
/// Обработчик команды <see cref="AddProjectMethodsCommand"/>.
/// </summary>
internal sealed class AddProjectMethodsCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IMethodRepository methodRepository,
    IProjectMethodRepository projectMethodRepository,
    ICalculationService calculationService,
    IUnitOfWork unitOfWork) : ICommandHandler<AddProjectMethodsCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(AddProjectMethodsCommand request, CancellationToken cancellationToken)
    {
        // Получение проекта по его идентификатору
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        // Проверка владения или членства пользователя в проекте
        Result? isOwnerResult = project.IsOwner(currentUserService.Id!);
        Result? isMemberResult = project.IsMember(currentUserService.Id!);

        if (isOwnerResult.IsFailure && isMemberResult.IsFailure)
        {
            return isMemberResult;
        }

        // Проверка существования каждого метода
        foreach (Guid methodId in request.MethodIds)
        {
            if (!await methodRepository.Exists(methodId))
            {
                return Result.Failure(MethodErrors.NotFound);
            }
        }

        // Добавление методов в проект
        var results = request.MethodIds.Select(methodId => project.AddMethod(methodId)).ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        // Создание расчетов для каждого добавленного метода
        foreach (Guid methodId in request.MethodIds)
        {
            Domain.Methods.Method method = await methodRepository.GetByIdAsync(methodId, cancellationToken);
            Result result = await calculationService.Create(project, method!);
            if (result.IsFailure)
            {
                return Result.Failure(ValidationError.FromResults([result]));
            }
        }

        // Вставка связей между проектом и методами в репозиторий и сохранение изменений
        projectMethodRepository.InsertRange(results.Select(r => r.Value).ToList());
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
