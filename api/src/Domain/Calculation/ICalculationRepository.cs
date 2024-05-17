using System.Linq.Expressions;

namespace Domain.Calculation;

/// <summary>
/// Интерфейс репозитория для работы с расчетами.
/// </summary>
public interface ICalculationRepository
{
    /// <summary>
    /// Получение расчета по заданному предикату.
    /// </summary>
    /// <param name="predicate">Предикат для выбора расчета.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns>Задача, представляющая асинхронную операцию получения одного расчета.</returns>
    Task<Calculation?> GetOne(
        Expression<Func<Calculation, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает список расчетов, соответствующих заданному предикату.
    /// </summary>
    /// <param name="predicate">Предикат для выбора расчетов.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns>Задача, представляющая асинхронную операцию получения списка расчетов.</returns>
    Task<List<Calculation>> Get(Expression<Func<Calculation, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Вставляет новый расчет.
    /// </summary>
    /// <param name="calculation">Расчет для вставки.</param>
    void Insert(Calculation calculation);

    /// <summary>
    /// Обновляет существующий расчет.
    /// </summary>
    /// <param name="calculation">Расчет для обновления.</param>
    void Update(Calculation calculation);

    /// <summary>
    /// Удаляет существующий расчет.
    /// </summary>
    /// <param name="calculation">Расчет для удаления.</param>
    void Remove(Calculation calculation);
}
