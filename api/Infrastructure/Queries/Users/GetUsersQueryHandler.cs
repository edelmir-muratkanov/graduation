using Application.Users.GetUsers;
using Shared.Mappings;

namespace Infrastructure.Queries.Users;

internal sealed class GetUsersQueryHandler(ApplicationReadDbContext dbContext)
    : IQueryHandler<GetUsersQuery, PaginatedList<UserResponse>>
{
    public async Task<Result<PaginatedList<UserResponse>>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<UserReadModel> usersQuery = dbContext.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            usersQuery = usersQuery.Where(u => EF.Functions.ILike(u.Email, $"%{request.SearchTerm}%"));
        }


        usersQuery = request.SortOrder == SortOrder.Desc
            ? usersQuery.OrderByDescending(u => u.Email)
            : usersQuery.OrderBy(u => u.Email);

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
