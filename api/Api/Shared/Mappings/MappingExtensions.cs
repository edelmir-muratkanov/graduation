using Api.Shared.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Api.Shared.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        IConfigurationProvider configuration,
        CancellationToken cancellationToken = default)
    {
        return queryable.ProjectTo<TDestination>(configuration).ToListAsync(cancellationToken);
    }
}