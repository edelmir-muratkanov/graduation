namespace Application.Property.Create;

/// <summary>
/// Ответ на команду <see cref="CreatePropertyCommand"/>
/// </summary>
public record CreatePropertyResponse(Guid Id, string Name, string Unit);

/// <summary>
/// Команда для создания свойства.
/// </summary>
/// <param name="Name">Название свойства</param>
/// <param name="Unit">Единица измерения свойства</param>
public record CreatePropertyCommand(string Name, string Unit)
    : ICommand<CreatePropertyResponse>;
