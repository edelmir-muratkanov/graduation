using Domain;
using Domain.Methods;

namespace Application.Method.GetMethodById;

public record GetMethodByIdParameterResponse
{
    public string PropertyName { get; set; }
    public ParameterValueGroup? First { get; set; }
    public ParameterValueGroup? Second { get; set; }
}

public record GetMethodByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CollectorType> CollectorTypes { get; set; }
    public List<GetMethodByIdParameterResponse> Parameters { get; set; }
}

public record GetMethodByIdQuery : IQuery<GetMethodByIdResponse>
{
    public Guid Id { get; set; }
}