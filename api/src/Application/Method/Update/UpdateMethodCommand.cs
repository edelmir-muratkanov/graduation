using Domain;

namespace Application.Method.Update;

/// <summary>
/// Команда для обновления метода.
/// </summary>
/// <param name="Id">Идентификатор метода</param>
/// <param name="Name">Новое название метода</param>
/// <param name="CollectorTypes">Новые типы коллектора метода</param>
public record UpdateMethodCommand(
    Guid Id,
    string? Name,
    List<CollectorType>? CollectorTypes) : ICommand;
