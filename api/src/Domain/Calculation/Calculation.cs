namespace Domain.Calculation;

/// <summary>
/// Представляет вычисление применимости метода к проекту
/// </summary>
public class Calculation : Entity
{
    private readonly List<CalculationItem> _items = [];

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    /// <remarks>
    /// Необходим для корректной работы EF Core
    /// </remarks>
    private Calculation() { }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Calculation"/>.
    /// </summary>
    /// <param name="projectId">Уникальный идентификатор проекта, связанного с вычислением.</param>
    /// <param name="methodId">Уникальный идентификатор метода, связанного с вычислением.</param>
    private Calculation(Guid projectId, Guid methodId)
    {
        ProjectId = projectId;
        MethodId = methodId;
    }

    /// <summary>
    /// Уникальный идентификатор проекта, связанного с вычислением.
    /// </summary>
    public Guid ProjectId { get; private set; }

    /// <summary>
    /// Уникальный идентификатор метода, связанного с вычислением.
    /// </summary>
    public Guid MethodId { get; private set; }

    /// <summary>
    /// Получает степень принадлежности для всего вычисления на основе степеней принадлежности его элементов.
    /// </summary>
    public Belonging? Belonging =>
        _items.Count != 0
            ? new Belonging((double)1 / _items.Count * _items
                .Sum(i => i.Belonging.Degree))
            : null;

    /// <summary>
    /// Коллекцию элементов вычисления.
    /// </summary>
    public IEnumerable<CalculationItem> Items => _items.ToList();

    /// <summary>
    /// Создает новое вычисление.
    /// </summary>
    /// <param name="projectId">Уникальный идентификатор проекта, связанного с вычислением.</param>
    /// <param name="methodId">Уникальный идентификатор метода, связанного с вычислением.</param>
    /// <returns>Новый экземпляр класса <see cref="Calculation"/>.</returns>
    public static Calculation Create(Guid projectId, Guid methodId)
    {
        var calculation = new Calculation(projectId, methodId);
        return calculation;
    }

    /// <summary>
    /// Добавляет новый элемент в вычисление.
    /// </summary>
    /// <param name="propertyName">Название свойства, связанного с элементом вычисления.</param>
    /// <param name="belonging">Степень принадлежности элемента вычисления.</param>
    /// <returns>Результат операции, содержащий новый элемент вычисления или информацию об ошибке.</returns>
    public Result<CalculationItem> AddItem(string propertyName, Belonging belonging)
    {
        if (_items.Any(i => i.PropertyName == propertyName))
        {
            return Result.Failure<CalculationItem>(CalculationErrors.DuplicateItems);
        }

        var item = CalculationItem.Create(Id, propertyName, belonging);

        _items.Add(item);

        return item;
    }

    /// <summary>
    /// Удаляет элемент из вычисления по названию свойства.
    /// </summary>
    /// <param name="propertyName">Название свойства элемента вычисления.</param>
    /// <returns>Результат операции, содержащий удаленный элемент вычисления или информацию об ошибке.</returns>
    public Result<CalculationItem> RemoveItem(string propertyName)
    {
        CalculationItem? item = _items.FirstOrDefault(i => i.PropertyName == propertyName);
        if (item is null)
        {
            return Result.Failure<CalculationItem>(CalculationErrors.ItemNotFound);
        }

        _items.Remove(item);

        return item;
    }

    /// <summary>
    /// Обновляет степень принадлежности элемента вычисления по его названию свойства.
    /// </summary>
    /// <param name="propertyName">Название свойства элемента вычисления.</param>
    /// <param name="belonging">Новая степень принадлежности элемента вычисления.</param>
    /// <returns>Результат операции, успешность которой зависит от обновления элемента вычисления.</returns>
    public Result UpdateItem(string propertyName, Belonging belonging)
    {
        CalculationItem? item = _items.FirstOrDefault(i => i.PropertyName == propertyName);
        if (item is null)
        {
            return Result.Failure(CalculationErrors.ItemNotFound);
        }

        item.UpdateBelonging(belonging);

        return Result.Success();
    }
}
