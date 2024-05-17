using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Project.Create;

/// <summary>
/// Обработчик команды <see cref="CreateProjectCommand"/>.
/// </summary>
internal class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProjectCommand, Guid>
{
    /// <inheritdoc/>
    public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        // Создание нового проекта с помощью фабричного метода
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

        // Вставляем новый проект в репозиторий
        projectRepository.Insert(projectResult.Value);

        // Сохраняем изменения в базе данных
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(projectResult.Value.Id);
    }
}
