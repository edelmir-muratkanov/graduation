namespace Application.Method.Delete;

public record DeleteMethodCommand : ICommand
{
    public Guid Id { get; set; }
}
