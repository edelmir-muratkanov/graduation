namespace Application.Property.GetById;

public record GetPropertyByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
};

public record GetPropertyByIdQuery(Guid Id) : IQuery<GetPropertyByIdResponse>;
