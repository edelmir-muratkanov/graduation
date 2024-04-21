using Api.Shared;

namespace Api.Domain.Properties;

public class Property : AuditableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
}