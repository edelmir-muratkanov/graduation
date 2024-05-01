namespace Application.Project.AddMethods;

public record AddProjectMethodsCommand : ICommand
{
    public required Guid ProjectId { get; set; }
    public required List<Guid> MethodIds { get; set; }
}
