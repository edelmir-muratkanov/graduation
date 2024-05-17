using Domain.Properties;

namespace Application.Property.Create;

/// <summary>
/// Обработчик команды создания свойства.
/// </summary>
internal sealed class CreatePropertyCommandHandler(
    IPropertyRepository propertyRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreatePropertyCommand, CreatePropertyResponse>
{
    public async Task<Result<CreatePropertyResponse>> Handle(CreatePropertyCommand request,
        CancellationToken cancellationToken)
    {
        // Проверка уникальности названия свойства
        if (!await propertyRepository.IsNameUniqueAsync(request.Name))
        {
            return Result.Failure<CreatePropertyResponse>(PropertyErrors.NameNotUnique);
        }

        // Создание свойства
        Result<Domain.Properties.Property>? propertyResult =
            Domain.Properties.Property.Create(request.Name, request.Unit);

        if (propertyResult.IsFailure)
        {
            return Result.Failure<CreatePropertyResponse>(propertyResult.Error);
        }

        // Вставляем созданное свойство в репозиторий
        propertyRepository.Insert(propertyResult.Value);

        // Сохраняем изменения в БД
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreatePropertyResponse(
            propertyResult.Value.Id,
            propertyResult.Value.Name,
            propertyResult.Value.Unit);
    }
}
