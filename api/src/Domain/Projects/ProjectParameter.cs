namespace Domain.Projects;

/// <summary>
/// Представляет параметр проекта.
/// </summary>
public class ProjectParameter : Entity
{
    /// <summary>
    /// Инициализирует новый экземпляр класса ProjectParameter.
    /// </summary>
    /// <param name="id">Идентификатор параметра.</param>
    /// <param name="projectId">Идентификатор проекта, к которому относится параметр.</param>
    /// <param name="propertyId">Идентификатор свойства, которое представляет параметр.</param>
    /// <param name="value">Значение параметра.</param>
    private ProjectParameter(Guid id, Guid projectId, Guid propertyId, double value) : base(id)
    {
        ProjectId = projectId;
        PropertyId = propertyId;
        Value = value;
    }

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private ProjectParameter()
    {
    }

    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public Guid ProjectId { get; private set; }

    /// <summary>
    /// Идентификатор свойства, которое представляет параметр.
    /// </summary>
    public Guid PropertyId { get; private set; }

    /// <summary>
    /// Значение параметра.
    /// </summary>
    public double Value { get; private set; }

    /// <summary>
    /// Создает новый экземпляр класса ProjectParameter.
    /// </summary>
    /// <param name="projectId">Идентификатор проекта, к которому относится параметр.</param>
    /// <param name="propertyId">Идентификатор свойства, которое представляет параметр.</param>
    /// <param name="value">Значение параметра.</param>
    /// <returns>Результат создания параметра проекта.</returns>
    public static Result<ProjectParameter> Create(Guid projectId, Guid propertyId, double value)
    {
        var parameter = new ProjectParameter(Guid.NewGuid(), projectId, propertyId, value);
        return parameter;
    }
}
