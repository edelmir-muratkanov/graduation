using Microsoft.EntityFrameworkCore;

namespace Shared;

/// <summary>
/// Представляет собой список элементов с поддержкой постраничной навигации.
/// </summary>
/// <param name="items">Список элементов на текущей странице.</param>
/// <param name="count">Общее количество элементов.</param>
/// <param name="pageNumber">Номер текущей страницы.</param>
/// <param name="pageSize">Количество элементов на одной странице.</param>
/// <typeparam name="T">Тип элементов в списке</typeparam>
public class PaginatedList<T>(List<T> items, int count, int pageNumber, int pageSize)
{
    /// <summary>
    /// Список элементов на текущей странице.
    /// </summary>
    public List<T> Items { get; } = items;

    /// <summary>
    /// Номер текущей страницы.
    /// </summary>
    public int PageNumber { get; } = pageNumber;

    /// <summary>
    /// Общее количество страниц.
    /// </summary>
    public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public int TotalCount { get; } = count;

    /// <summary>
    /// Определяет, есть ли предыдущая страница.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Определяет, есть ли следующая страница.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Асинхронно создает новый экземпляр <see cref="PaginatedList{T}"/> на основе указанного источника данных.
    /// </summary>
    /// <param name="source">Источник данных для пагинации.</param>
    /// <param name="pageNumber">Номер текущей страницы.</param>
    /// <param name="pageSize">Количество элементов на одной странице</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Асинхронная задача, возвращающая новый экземпляр <see cref="PaginatedList{T}"/>.</returns>
    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken);
        List<T>? items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
