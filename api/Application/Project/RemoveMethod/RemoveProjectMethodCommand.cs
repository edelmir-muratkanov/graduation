namespace Application.Project.RemoveMethod;

public sealed record RemoveProjectMethodCommand : ICommand
{
    public required Guid ProjectId { get; set; }
    public required Guid MethodId { get; set; }
}
