using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.Update;

/// <summary>
/// Обработчик команды <see cref="UpdateProjectCommand"/>.
/// </summary>
internal sealed class UpdateProjectCommandHandler(
    ICurrentUserService currentUserService,
    IProjectRepository projectRepository,
    IMethodRepository methodRepository,
    ICalculationService calculationService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProjectCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        // Получение проекта по его идентификатору
        Domain.Projects.Project? project = await projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
        {
            return Result.Failure(ProjectErrors.NotFound);
        }

        // Получение идентификатора текущего пользователя
        string userId = currentUserService.Id ?? string.Empty;

        // Проверка, является ли текущий пользователь владельцем проекта
        Result isOwnerResult = project.IsOwner(userId);

        if (isOwnerResult.IsFailure)
        {
            return isOwnerResult;
        }

        // Обновление основной информации о проекте
        project.UpdateBaseInfo(
            request.Name,
            request.Country,
            request.Operator,
            request.ProjectType,
            request.CollectorType);

        // Обновление связанных вычислений
        foreach (ProjectMethod projectMethod in project.Methods)
        {
            // Получение метода проекта по его идентификатору
            Domain.Methods.Method? method = await methodRepository
                .GetByIdAsync(projectMethod.MethodId, cancellationToken);

            // Вызов сервиса обновления вычислений
            Result result = await calculationService.Update(project, method!);
            if (result.IsFailure)
            {
                return result;
            }
        }

        // Сохранение изменений в репозитории
        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
