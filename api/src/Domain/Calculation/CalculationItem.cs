namespace Domain.Calculation;

/// <summary>
/// Представляет элемент вычисления, связанный с определенным свойством.
/// </summary>
public class CalculationItem : Entity
{
    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private CalculationItem() { }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="CalculationItem"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор элемента вычисления.</param>
    /// <param name="calculationId">Уникальный идентификатор связанного вычисления.</param>
    /// <param name="propertyName">Название свойства, связанного с элементом вычисления.</param>
    /// <param name="degree">Степень принадлежности элемента вычисления.</param>
    private CalculationItem(Guid id, Guid calculationId, string propertyName, Belonging degree) : base(id)
    {
        CalculationId = calculationId;
        PropertyName = propertyName;
        Belonging = degree;
    }

    /// <summary>
    /// Уникальный идентификатор связанного вычисления.
    /// </summary>
    public Guid CalculationId { get; private set; }

    /// <summary>
    /// Название свойства, связанного с элементом вычисления.
    /// </summary>
    public string PropertyName { get; private set; }

    /// <summary>
    /// Степень принадлежности элемента вычисления.
    /// </summary>
    public Belonging Belonging { get; private set; }

    /// <summary>
    /// Создает новый элемент вычисления.
    /// </summary>
    /// <param name="calculationId">Уникальный идентификатор связанного вычисления.</param>
    /// <param name="propertyName">Название свойства, связанного с элементом вычисления.</param>
    /// <param name="degree">Степень принадлежности элемента вычисления.</param>
    /// <returns>Новый экземпляр класса <see cref="CalculationItem"/>.</returns>
    public static CalculationItem Create(Guid calculationId, string propertyName, Belonging degree)
    {
        var item = new CalculationItem(Guid.NewGuid(), calculationId, propertyName, degree);

        return item;
    }

    /// <summary>
    /// Обновляет степень принадлежности элемента вычисления.
    /// </summary>
    /// <param name="belonging">Новая степень принадлежности элемента вычисления.</param>
    public void UpdateBelonging(Belonging belonging)
    {
        Belonging = belonging;
    }
}
