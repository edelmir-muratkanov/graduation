namespace Infrastructure.Database.Models;

internal sealed class ProjectParameterReadModel
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid PropertyId { get; set; }
    public PropertyReadModel Property { get; set; }
    public double Value { get; set; }
}