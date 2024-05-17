using Domain.Methods;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с параметрами методов.
/// </summary>
internal sealed class MethodParameterRepository(ApplicationWriteDbContext context)
    : IMethodParameterRepository
{
    /// <inheritdoc />
    public void InsertRange(IEnumerable<MethodParameter> parameters)
    {
        context.MethodParameters.AddRange(parameters);
    }

    /// <inheritdoc />
    public void RemoveRange(IEnumerable<MethodParameter> parameters)
    {
        context.MethodParameters.RemoveRange(parameters);
    }

    /// <inheritdoc />
    public void UpdateRange(IEnumerable<MethodParameter> parameters)
    {
        context.MethodParameters.UpdateRange(parameters);
    }
}
