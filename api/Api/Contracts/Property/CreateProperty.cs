namespace Api.Contracts.Property;

public record CreatePropertyRequest(string Name, string Unit);

public record CreatePropertyResponse(Guid Id, string Name, string Unit);
