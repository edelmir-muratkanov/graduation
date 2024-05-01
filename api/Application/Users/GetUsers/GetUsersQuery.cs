using Shared;

namespace Application.Users.GetUsers;

public record UserResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
}

public record GetUsersQuery : IQuery<PaginatedList<UserResponse>>
{
    public string? SearchTerm { get; init; }
    public SortOrder? SortOrder { get; init; }
    public int? PageNumber { get; init; }
    public int? PageSize { get; init; }
};
