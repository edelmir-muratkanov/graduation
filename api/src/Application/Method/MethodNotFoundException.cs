namespace Application.Method;

/// <summary>
/// Исключение, возникающее при отсутствии метода.
/// </summary>
/// <param name="methodId">Идентификатор метода, который не был найден.</param>

public sealed class MethodNotFoundException(Guid methodId) : Exception($"Method with id = {methodId} not found")
{
    /// <summary>
    /// Идентификатор метода, который не был найден.
    /// </summary>
    public Guid MethodId { get; set; } = methodId;
}
