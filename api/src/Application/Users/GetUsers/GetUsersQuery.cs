using Shared;

namespace Application.Users.GetUsers;

/// <summary>
/// Ответ, содержащий информацию о пользователе.
/// </summary>
public record UserResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
}

/// <summary>
/// Запрос на получение списка пользователей.
/// </summary>
public record GetUsersQuery : IQuery<PaginatedList<UserResponse>>
{
    public string? SearchTerm { get; init; }
    public SortOrder? SortOrder { get; init; }
    public int? PageNumber { get; init; }
    public int? PageSize { get; init; }
};
