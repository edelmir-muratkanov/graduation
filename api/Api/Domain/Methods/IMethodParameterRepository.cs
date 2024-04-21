namespace Api.Domain.Methods;

public interface IMethodParameterRepository
{
    void InsertRange(IEnumerable<MethodParameter> parameters);
}