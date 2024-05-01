namespace Application.Project.Remove;

public record RemoveProjectCommand : ICommand
{
    public required Guid ProjectId { get; init; }
}
