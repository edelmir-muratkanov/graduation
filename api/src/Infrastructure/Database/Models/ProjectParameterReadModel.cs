namespace Infrastructure.Database.Models;

/// <summary>
/// Модель параметра проекта для операций чтения
/// </summary>
internal class ProjectParameterReadModel
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid PropertyId { get; set; }
    public PropertyReadModel Property { get; set; }
    public double Value { get; set; }
}
