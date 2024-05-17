using Domain.Calculation;
using Domain.Properties;

namespace Application.Property.Delete;

/// <summary>
/// Обработчик команды удаления свойства.
/// </summary>
internal sealed class DeletePropertyCommandHandler(
    IPropertyRepository propertyRepository,
    ICalculationRepository calculationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePropertyCommand>
{
    public async Task<Result> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        // Получение свойства по его идентификатору
        Domain.Properties.Property? property = await propertyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (property is null)
        {
            return Result.Failure(PropertyErrors.NotFound);
        }

        // Удаление свойства из репозитория
        propertyRepository.Remove(property);

        // Получение всех расчетов, использующих данное свойство
        List<Calculation> calculations = await calculationRepository.Get(
            calculation => calculation.Items.Any(i => i.PropertyName == property.Name), cancellationToken);

        // Обновление расчетов, удаляя свойство из списков используемых
        foreach (Calculation calculation in calculations)
        {
            calculation.RemoveItem(property.Name);
            calculationRepository.Update(calculation);
        }

        // Сохранение изменений
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
