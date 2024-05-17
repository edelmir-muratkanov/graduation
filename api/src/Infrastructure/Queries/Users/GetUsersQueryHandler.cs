using Application.Users.GetUsers;
using Shared.Mappings;

namespace Infrastructure.Queries.Users;

/// <summary>
/// Обработчик запроса <see cref="GetUsersQuery"/>
/// </summary>
internal sealed class GetUsersQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetUsersQuery, PaginatedList<UserResponse>>
{
    public async Task<Result<PaginatedList<UserResponse>>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        // Запрос на получение списка пользователей
        IQueryable<UserReadModel> usersQuery = dbContext.Users.AsQueryable();

        // Применение фильтрации по поисковому запросу, если он задан
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            usersQuery = usersQuery.Where(u => EF.Functions.ILike(u.Email, $"%{request.SearchTerm}%"));
        }
        
        // Применение сортировки
        usersQuery = request.SortOrder == SortOrder.Desc
            ? usersQuery.OrderByDescending(u => u.Email)
            : usersQuery.OrderBy(u => u.Email);

        // Выполнение запроса с пагинацией
        PaginatedList<UserResponse> users = await usersQuery
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email
            })
            .PaginatedListAsync(
                request.PageNumber ?? 1,
                request.PageSize ?? 10,
                cancellationToken);


        return users;
    }
}
