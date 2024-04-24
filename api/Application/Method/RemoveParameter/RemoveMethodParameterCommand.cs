namespace Application.Method.RemoveParameter;

public record RemoveMethodParameterCommand : ICommand
{
    public Guid MethodId { get; set; }
    public Guid ParameterId { get; set; }
}