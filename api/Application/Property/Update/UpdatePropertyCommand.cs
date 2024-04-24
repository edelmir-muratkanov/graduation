namespace Application.Property.Update;

public record UpdatePropertyCommand(Guid Id, string? Name, string? Unit) : ICommand;
