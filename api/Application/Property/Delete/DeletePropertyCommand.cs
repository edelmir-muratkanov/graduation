namespace Application.Property.Delete;

public record DeletePropertyCommand(Guid Id) : ICommand;
