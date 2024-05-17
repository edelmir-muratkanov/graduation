using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;
using Domain.Properties;

namespace Application.Method.AddParameters;

/// <summary>
/// Обработчик команды <see cref="AddMethodParametersCommand"/>.
/// </summary>
/// <param name="methodRepository">Репозиторий методов.</param>
/// <param name="propertyRepository">Репозиторий свойств.</param>
/// <param name="methodParameterRepository">Репозиторий параметров метода.</param>
/// <param name="calculationRepository">Репозиторий вычислений.</param>
/// <param name="calculationService">Сервис для выполнения вычислений.</param>
/// <param name="projectRepository">Репозиторий проектов.</param>
/// <param name="unitOfWork">Unit of Work для сохранений изменений в БД</param>
internal class AddMethodParametersCommandHandler(
    IMethodRepository methodRepository,
    IPropertyRepository propertyRepository,
    IMethodParameterRepository methodParameterRepository,
    ICalculationRepository calculationRepository,
    ICalculationService calculationService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddMethodParametersCommand>
{
    /// <inheritdoc />
    public async Task<Result> Handle(AddMethodParametersCommand request, CancellationToken cancellationToken)
    {
        // Получаем метод по его идентификатору.
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        // Проверяем существование свойств параметров.
        foreach (MethodParameter? parameter in request.Parameters)
        {
            if (!await propertyRepository.Exists(parameter.PropertyId))
            {
                return Result.Failure(MethodErrors.NotFoundParameter);
            }
        }

        // Добавляем параметры к методу.
        var results = request.Parameters.Select(parameterRequest =>
                method.AddParameter(
                    parameterRequest.PropertyId,
                    parameterRequest.First,
                    parameterRequest.Second))
            .ToList();

        // Проверяем результаты операции.
        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        // Получаем все вычисления, связанные с этим методом.
        List<Calculation> calculations = await calculationRepository.Get(
            c => c.MethodId == method.Id,
            cancellationToken);

        // Обновляем вычисления после добавления параметров.
        foreach (Calculation calculation in calculations)
        {
            // Получаем проект, связанный с вычислением.
            Domain.Projects.Project? project = await projectRepository
                .GetByIdAsync(calculation.ProjectId, cancellationToken);

            // Обновляем вычисление для проекта.
            Result result = await calculationService.Update(project!, method);

            if (result.IsFailure)
            {
                return result;
            }
        }

        // Вставляем параметры в репозиторий параметров метода и сохраняем изменения в базе данных.
        methodParameterRepository.InsertRange(results.Select(r => r.Value));
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
