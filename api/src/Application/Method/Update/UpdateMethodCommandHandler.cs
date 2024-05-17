using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Method.Update;

/// <summary>
/// Обработчик команды <see cref="UpdateMethodCommand"/>
/// </summary>
internal sealed class UpdateMethodCommandHandler(
    IMethodRepository methodRepository,
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    ICalculationService calculationService,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMethodCommand>
{
    /// <inheritdoc />
    public async Task<Result> Handle(UpdateMethodCommand request, CancellationToken cancellationToken)
    {
        // Получаем метод по его идентификатору
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.Id, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        // Обновляем имя и типы коллектора метода
        method.ChangeNameAndCollectorTypes(request.Name, request.CollectorTypes);

        // Получаем все расчеты, связанные с этим методом
        List<Calculation> calculations = await calculationRepository.Get(
            c => c.MethodId == method.Id,
            cancellationToken);

        // Для каждого расчета обновляем расчеты
        foreach (Calculation calculation in calculations)
        {
            // Получаем проект, связанный с расчетом
            Domain.Projects.Project? project = await projectRepository
                .GetByIdAsync(calculation.ProjectId, cancellationToken);

            // Обновляем расчет
            Result result = await calculationService.Update(project!, method);

            if (result.IsFailure)
            {
                return result;
            }
        }

        // Обновляем и сохраняем метод в БД
        methodRepository.Update(method);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
