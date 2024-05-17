namespace Domain.Properties;

/// <summary>
/// Представляет сущность геолого-физического свойства
/// </summary>
/// <example>
/// Температра, проницаемость, глубина залегания и другие.
/// </example>
public class Property : AuditableEntity
{
    /// <summary>
    /// Создает новый экземпляр класса <see cref="Property"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор свойства.</param>
    /// <param name="name">Название свойства.</param>
    /// <param name="unit">Единица измерения свойства.</param>
    private Property(Guid id, string name, string unit) : base(id)
    {
        Id = id;
        Name = name;
        Unit = unit;
    }

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private Property()
    {
    }

    /// <summary>
    /// Название свойства
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Единица измерения свойства.
    /// </summary>
    public string Unit { get; private set; }

    /// <summary>
    /// Создает новое свойство.
    /// </summary>
    /// <param name="name">Название свойства.</param>
    /// <param name="unit">Единица измерения свойства.</param>
    /// <returns>Результат операции, содержащий новое свойство или информацию об ошибке.</returns>
    public static Result<Property> Create(string name, string unit)
    {
        return new Property(Guid.NewGuid(), name, unit);
    }

    /// <summary>
    /// Обновляет свойство с новыми данными.
    /// </summary>
    /// <param name="name">Новое название свойства.</param>
    /// <param name="unit">Новая единица измерения свойства.</param>
    /// <returns>Результат операции, успешность которой зависит от обновления свойства.</returns>
    public Result Update(string? name, string? unit)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }

        if (!string.IsNullOrWhiteSpace(unit))
        {
            Unit = unit;
        }

        return Result.Success();
    }
}
