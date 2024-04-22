using Application.Abstractions.Authentication;
using Domain.Users;

namespace Application.Auth.Refresh;

internal sealed class RefreshCommandHandler(
    IJwtTokenProvider jwtTokenProvider,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RefreshCommand, RefreshResponse>
{
    public async Task<Result<RefreshResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var userId = await jwtTokenProvider.GetUserFromToken(request.AccessToken);

        if (userId is null) return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);

        if (!Guid.TryParse(userId, out var id)) return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);

        var user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null || user.Token != request.RefreshToken)
            return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);

        var access = jwtTokenProvider.Generate(user);
        var refresh = jwtTokenProvider.GenerateRefreshToken();

        user.UpdateToken(refresh);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RefreshResponse(access, refresh);
    }
}