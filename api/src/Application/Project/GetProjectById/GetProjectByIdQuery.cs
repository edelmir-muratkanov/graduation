using Domain;
using Domain.Projects;

namespace Application.Project.GetProjectById;

/// <summary>
/// Вспомогательный класс для ответа
/// </summary>
public record GetProjectByIdMember
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
}

/// <summary>
/// Вспомогательный класс для ответа
/// </summary>
public record GetProjectByIdMethod
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

/// <summary>
/// Вспомогательный класс для ответа
/// </summary>
public record GetProjectByIdParameter
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Unit { get; set; }
    public required double Value { get; set; }
}

/// <summary>
/// Ответ на запрос <see cref="GetProjectByIdQuery"/>
/// </summary>
public record GetProjectByIdResponse
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Operator { get; set; }
    public required ProjectType Type { get; set; }
    public required Guid OwnerId { get; set; }
    public required CollectorType CollectorType { get; set; }
    public required List<GetProjectByIdMember> Members { get; set; }
    public required List<GetProjectByIdMethod> Methods { get; set; }
    public required List<GetProjectByIdParameter> Parameters { get; set; }
}

/// <summary>
/// Запрос на получение проекта по его идентификатору.
/// </summary>
public record GetProjectByIdQuery : IQuery<GetProjectByIdResponse>
{
    /// <summary>
    /// Идентификатор проекта
    /// </summary>
    public required Guid Id { get; init; }
}
