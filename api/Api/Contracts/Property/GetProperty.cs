namespace Api.Contracts.Property;

public record GetPropertyByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
};