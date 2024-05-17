namespace Shared;

/// <summary>
/// Представляет базовый класс для сущностей домена с поддержкой доменных событий.
/// </summary>
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Entity"/> с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    protected Entity(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Entity"/>.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    protected Entity()
    {
    }

    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Guid Id { get; protected init; }

    /// <summary>
    /// Список доменных событий, связанных с сущностью.
    /// </summary>
    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    /// <summary>
    /// Очищает список доменных событий.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Добавляет доменное событие в список событий.
    /// </summary>
    /// <param name="domainEvent">Доменное событие для добавления.</param>
    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
