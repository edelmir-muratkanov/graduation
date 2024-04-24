using Application.Abstractions.Authentication;
using Domain.Users;

namespace Application.Auth.Register;

internal sealed class RegisterCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordManager passwordManager,
    IJwtTokenProvider jwtTokenProvider)
    : ICommandHandler<RegisterCommand, RegisterResponse>
{
    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.IsEmailUniqueAsync(request.Email))
        {
            return Result.Failure<RegisterResponse>(UserErrors.EmailNotUnique);
        }

        string? password = passwordManager.HashPassword(request.Password);
        Result<User>? userResult = User.Create(request.Email, password);

        if (userResult.IsFailure)
        {
            return Result.Failure<RegisterResponse>(userResult.Error);
        }


        string? token = jwtTokenProvider.Generate(userResult.Value);
        string? refresh = jwtTokenProvider.GenerateRefreshToken();

        userResult.Value.UpdateToken(refresh);

        userRepository.Insert(userResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterResponse(
            userResult.Value.Id,
            userResult.Value.Email,
            userResult.Value.Role.ToString(),
            token,
            refresh);
    }
}
