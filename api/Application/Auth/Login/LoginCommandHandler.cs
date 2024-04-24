using Application.Abstractions.Authentication;
using Domain.Users;

namespace Application.Auth.Login;

internal sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordManager passwordManager,
    IJwtTokenProvider jwtTokenProvider) : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail(request.Email));
        }

        if (!passwordManager.VerifyPassword(user.Password, request.Password))
        {
            return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
        }

        string? token = jwtTokenProvider.Generate(user);
        string? refresh = jwtTokenProvider.GenerateRefreshToken();

        user.UpdateToken(refresh);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponse(user.Id, user.Email, user.Role.ToString(), token, refresh);
    }
}
