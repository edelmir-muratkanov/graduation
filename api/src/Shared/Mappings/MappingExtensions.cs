using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Shared.Mappings;

/// <summary>
/// Содержит методы расширения для работы с маппингом и проекциями в Entity Framework Core.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Создает пагинированный список сущностей <typeparamref name="TDestination"/> на основе запроса <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <typeparam name="TDestination">Тип, в который происходит маппинг.</typeparam>
    /// <param name="queryable">Запрос <see cref="IQueryable{T}"/>, который необходимо пагинировать.</param>
    /// <param name="pageNumber">Номер страницы пагинации.</param>
    /// <param name="pageSize">Размер страницы пагинации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, возвращающая пагинированный список сущностей <typeparamref name="TDestination"/>.</returns>
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);
    }

    /// <summary>
    /// Проецирует результат запроса <see cref="IQueryable{T}"/> в список <typeparamref name="TDestination"/> с использованием указанного конфигурационного провайдера.
    /// </summary>
    /// <typeparam name="TDestination">Тип, в который происходит маппинг.</typeparam>
    /// <param name="queryable">Запрос <see cref="IQueryable{T}"/>, который необходимо преобразовать.</param>
    /// <param name="configuration">Провайдер конфигурации маппинга AutoMapper.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, возвращающая список <typeparamref name="TDestination"/> с данными из запроса.</returns>
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        IConfigurationProvider configuration,
        CancellationToken cancellationToken = default)
    {
        return queryable.ProjectTo<TDestination>(configuration).ToListAsync(cancellationToken);
    }
}
