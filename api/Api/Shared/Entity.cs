﻿namespace Api.Shared;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; protected init; }
    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

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

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}