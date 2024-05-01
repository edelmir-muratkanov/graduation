namespace Application.Method;

public sealed class MethodNotFoundException(Guid methodId) : Exception($"Method with id = {methodId} not found")
{
    public Guid MethodId { get; set; } = methodId;
}
