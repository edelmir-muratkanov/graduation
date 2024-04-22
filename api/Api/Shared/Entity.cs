namespace Api.Shared;

public abstract class Entity : IHasDomainEvent
{
    private readonly List<DomainEvent> _domainEvents = [];

    public Guid Id { get; protected init; }
    public List<DomainEvent> DomainEvents => _domainEvents.ToList();

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}