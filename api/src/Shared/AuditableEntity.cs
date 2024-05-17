namespace Shared;

/// <summary>
/// Представляет абстрактный класс для сущностей, поддерживающих аудит.
/// </summary>
/// <remarks>
/// Наследуется от <see cref="Entity"/> и добавляет свойства для отслеживания 
/// времени создания и обновления, а также пользователей, создавших и обновивших сущность.
/// </remarks>
public abstract class AuditableEntity : Entity
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuditableEntity"/> с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    protected AuditableEntity(Guid id) : base(id)
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuditableEntity"/>.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    protected AuditableEntity()
    {
    }

    /// <summary>
    /// Дата и время создания сущности.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя, создавшего сущность.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Дата и время последнего обновления сущности.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя, последним обновивший сущность.
    /// </summary>
    public string? UpdatedBy { get; set; }
}
