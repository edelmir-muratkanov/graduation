namespace Domain.Projects;

public record ProjectMethod(Guid ProjectId, Guid MethodId)
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid MethodId { get; set; } = MethodId;
}
