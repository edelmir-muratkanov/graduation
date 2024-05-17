namespace Application.Abstractions.Data;

/// <summary>
/// Интерфейс для работы с единицей работы (UnitOfWork).
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Асинхронно сохраняет все изменения, внесенные в контекст базы данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Число, представляющее количество измененных записей в базе данных.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
