using Domain.Methods;

namespace Infrastructure.Repositories;

internal sealed class MethodParameterRepository(ApplicationWriteDbContext context)
    : IMethodParameterRepository
{
    public void InsertRange(IEnumerable<MethodParameter> parameters)
    {
        context.MethodParameters.AddRange(parameters);
    }

    public void RemoveRange(IEnumerable<MethodParameter> parameters)
    {
        context.MethodParameters.RemoveRange(parameters);
    }

    public void UpdateRange(IEnumerable<MethodParameter> parameters)
    {
        context.MethodParameters.UpdateRange(parameters);
    }
}
