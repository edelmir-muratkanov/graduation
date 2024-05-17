namespace Application.Calculations.GetByProject;

/// <summary>
/// Элемент расчета для ответа на запрос <see cref="GetCalculationsByProjectQuery"/>
/// </summary>
public record CalculationItemResponse
{
    /// <summary>
    /// Наименование свойства.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Коэффициент элемента расчета.
    /// </summary>
    public double Ratio { get; set; }
}

/// <summary>
/// Ответ на запрос <see cref="GetCalculationsByProjectQuery"/>
/// </summary>
public record CalculationResponse
{
    /// <summary>
    /// Наименование метода.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Коэффициент расчета.
    /// </summary>
    public double? Ratio { get; set; }

    /// <summary>
    /// Применимость.
    /// </summary>
    public string? Applicability { get; set; }

    /// <summary>
    /// Список элементов расчета.
    /// </summary>
    public List<CalculationItemResponse> Items { get; set; }
}

/// <summary>
/// Запрос на получение расчетов по проекту.
/// </summary>
public record GetCalculationsByProjectQuery : IQuery<List<CalculationResponse>>
{
    /// <summary>
    /// Идентификатор проекта.
    /// </summary>
    public Guid ProjectId { get; init; }
}
