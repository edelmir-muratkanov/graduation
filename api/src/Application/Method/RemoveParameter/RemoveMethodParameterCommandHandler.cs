using Domain.Calculation;
using Domain.Methods;
using Domain.Properties;

namespace Application.Method.RemoveParameter;

/// <summary>
/// Обработчик команды <see cref="RemoveMethodParameterCommand"/>
/// </summary>
internal class RemoveMethodParameterCommandHandler(
    IMethodRepository methodRepository,
    ICalculationRepository calculationRepository,
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveMethodParameterCommand>
{
    /// <inheritdoc />
    public async Task<Result> Handle(RemoveMethodParameterCommand request, CancellationToken cancellationToken)
    {
        // Получение метода по его идентификатору
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        // Удаление параметра из метода
        Result<MethodParameter> result = method.RemoveParameter(request.ParameterId);

        if (result.IsFailure)
        {
            return result;
        }

        // Получение свойства, связанного с удаленным параметром
        Domain.Properties.Property? property = await propertyRepository
            .GetByIdAsync(result.Value.PropertyId, cancellationToken);

        // Получение списка расчетов, связанных с методом
        List<Calculation> calculations = await calculationRepository
            .Get(calculation => calculation.MethodId == method.Id, cancellationToken);

        // Удаление элементов расчетов, связанных с удаленным параметром
        foreach (Calculation calculation in calculations)
        {
            calculation.RemoveItem(property!.Name);
            calculationRepository.Update(calculation);
        }

        // Обновление метода и сохранение изменений в БД
        methodRepository.Update(method);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
