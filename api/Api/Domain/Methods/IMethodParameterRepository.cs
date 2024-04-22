namespace Api.Domain.Methods;

public interface IMethodParameterRepository
{
    void InsertRange(IEnumerable<MethodParameter> parameters);
    void RemoveRange(IEnumerable<MethodParameter> parameters);
    void UpdateRange(IEnumerable<MethodParameter> parameters);
}