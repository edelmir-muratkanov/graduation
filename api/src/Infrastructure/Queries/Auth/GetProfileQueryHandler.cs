using Application.Abstractions.Authentication;
using Application.Auth.GetProfile;
using Domain.Users;

namespace Infrastructure.Queries.Auth;

internal sealed class GetProfileQueryHandler(
    ICurrentUserService currentUserService,
    ApplicationReadDbContext dbContext)
    : IQueryHandler<GetProfileQuery, GetProfileResponse>
{
    public async Task<Result<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        UserReadModel? user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == currentUserService.Id, cancellationToken);

        return user is not null
            ? new GetProfileResponse(user.Id.ToString(), user.Email, user.Role.ToString())
            : Result.Failure<GetProfileResponse>(UserErrors.Unauthorized);
    }
}
