using Application.Abstractions.Authentication;
using Application.Auth.GetProfile;
using Domain.Users;

namespace Infrastructure.Queries.Auth;

/// <summary>
/// Обработчик запроса <see cref="GetProfileQuery"/>
/// </summary>
internal sealed class GetProfileQueryHandler(
    ICurrentUserService currentUserService,
    ApplicationReadDbContext dbContext)
    : IQueryHandler<GetProfileQuery, GetProfileResponse>
{
    public async Task<Result<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        // Поиск пользователя в базе данных
        UserReadModel? user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == currentUserService.Id, cancellationToken);

        // Возвращение профиля пользователя, если он найден
        return user is not null
            ? new GetProfileResponse(user.Id.ToString(), user.Email, user.Role.ToString())
            : Result.Failure<GetProfileResponse>(UserErrors.Unauthorized);
    }
}
