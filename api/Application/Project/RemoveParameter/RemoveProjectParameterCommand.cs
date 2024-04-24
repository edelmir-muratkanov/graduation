namespace Application.Project.RemoveParameter;

public sealed record RemoveProjectParameterCommand : ICommand
{
    public Guid ProjectId { get; set; }
    public Guid ParameterId { get; set; }
}