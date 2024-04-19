namespace Api.Contracts.Property;

public record GetPropertyByIdResponse(Guid Id, string Name, string Unit);