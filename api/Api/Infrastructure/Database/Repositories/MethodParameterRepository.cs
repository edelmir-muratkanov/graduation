using Api.Domain.Methods;

namespace Api.Infrastructure.Database.Repositories;

internal class MethodParameterRepository(ApplicationDbContext context) : IMethodParameterRepository
{
    public void InsertRange(IEnumerable<MethodParameter> parameters) => context.MethodParameters.AddRange(parameters);
}