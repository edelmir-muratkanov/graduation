namespace Application.Property.Update;

/// <summary>
/// Команда для обновления свойства.
/// </summary>
public record UpdatePropertyCommand(Guid Id, string? Name, string? Unit) : ICommand;
