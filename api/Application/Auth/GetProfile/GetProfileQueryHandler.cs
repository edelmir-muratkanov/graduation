using Application.Abstractions.Authentication;

namespace Application.Auth.GetProfile;

internal sealed class GetProfileQueryHandler(ICurrentUserService currentUserService)
    : IQueryHandler<GetProfileQuery, GetProfileResponse>
{
    public Task<Result<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        if (currentUserService.Id != null && currentUserService is { Role: not null, Email: not null })
            return Task.FromResult<Result<GetProfileResponse>>(new GetProfileResponse(currentUserService.Id,
                currentUserService.Email, currentUserService.Role));

        return null!;
    }
}