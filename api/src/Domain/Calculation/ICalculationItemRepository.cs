namespace Domain.Calculation;

/// <summary>
/// Интерфейс репозитория для работы с элементами расчета.
/// </summary>
public interface ICalculationItemRepository
{
    /// <summary>
    /// Удаляет указанный элемент расчета.
    /// </summary>
    /// <param name="calculationItem">Элемент расчета для удаления.</param>
    void Remove(CalculationItem calculationItem);

    /// <summary>
    /// Вставляет новый элемент расчета.
    /// </summary>
    /// <param name="calculationItem">Элемент расчета для вставки.</param>
    void Insert(CalculationItem calculationItem);

    /// <summary>
    /// Вставляет коллекцию элементов расчета.
    /// </summary>
    /// <param name="calculationItems">Коллекция элементов расчета для вставки.</param>
    void InsertRange(IEnumerable<CalculationItem> calculationItems);

    /// <summary>
    /// Удаляет коллекцию элементов расчета.
    /// </summary>
    /// <param name="calculationItems">Коллекция элементов расчета для удаления.</param>
    void RemoveRange(IEnumerable<CalculationItem> calculationItems);
}
