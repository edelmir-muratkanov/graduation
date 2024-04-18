namespace Api.Shared;

public abstract class DomainEvent
{
    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccured { get; protected set; } = DateTimeOffset.UtcNow;
}

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; }
}