using Domain;

namespace Application.Method.Update;

public record UpdateMethodCommand(
    Guid Id,
    string? Name,
    List<CollectorType>? CollectorTypes) : ICommand;