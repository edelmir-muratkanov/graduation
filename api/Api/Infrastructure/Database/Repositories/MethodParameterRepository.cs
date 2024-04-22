using Api.Domain.Methods;

namespace Api.Infrastructure.Database.Repositories;

internal class MethodParameterRepository(ApplicationDbContext context) : IMethodParameterRepository
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