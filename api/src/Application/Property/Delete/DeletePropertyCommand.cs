namespace Application.Property.Delete;

/// <summary>
/// Команда на удаление свойства.
/// </summary>
public record DeletePropertyCommand(Guid Id) : ICommand;
