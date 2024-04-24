using Domain.Methods;

namespace Infrastructure.Database.Models;

internal sealed class MethodParameterReadModel
{
    public Guid Id { get; set; }
    public Guid MethodId { get; set; }
    public Guid PropertyId { get; set; }
    public PropertyReadModel Property { get; set; }
    public ParameterValueGroup? FirstParameters { get; set; }
    public ParameterValueGroup? SecondParameters { get; set; }
}
