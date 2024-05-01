namespace Application.Project.AddParameters;

public record AddProjectParameter
{
    public required Guid PropertyId { get; set; }
    public required double Value { get; set; }
}

public record AddProjectParametersCommand : ICommand
{
    public required Guid ProjectId { get; set; }
    public required List<AddProjectParameter> Parameters { get; set; }
}
