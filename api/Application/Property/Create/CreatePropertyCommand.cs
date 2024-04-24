namespace Application.Property.Create;

public record CreatePropertyResponse(Guid Id, string Name, string Unit);

public record CreatePropertyCommand(string Name, string Unit)
    : ICommand<CreatePropertyResponse>;
