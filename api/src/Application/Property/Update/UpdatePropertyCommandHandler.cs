using Domain.Properties;

namespace Application.Property.Update;

/// <summary>
/// Обработчик команды <see cref="UpdatePropertyCommand"/>
/// </summary>
internal class UpdatePropertyCommandHandler(
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePropertyCommand>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        // Получение свойства по его идентификатору
        Domain.Properties.Property? property = await propertyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (property is null)
        {
            return Result.Failure(PropertyErrors.NotFound);
        }

        // Обновление свойства и получение результата
        Result? propertyResult = property.Update(request.Name, request.Unit);

        if (propertyResult.IsFailure)
        {
            return propertyResult;
        }

        // Обновление свойства в репозитории
        propertyRepository.Update(property);

        // Сохранение изменений в базе данных
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
