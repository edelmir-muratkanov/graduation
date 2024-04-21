namespace Api.Domain.Methods;

public class MethodParameter
{
    public Guid MethodId { get; set; }
    public Guid PropertyId { get; set; }
    public ParameterValueGroup? FirstParameters { get; set; }
    public ParameterValueGroup? SecondParameters { get; set; }
}