using Domain.Users;

namespace Application.Auth.Login;

/// <summary>
/// Обработчик команды входа в систему.
/// </summary>
internal sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordManager passwordManager,
    IJwtTokenProvider jwtTokenProvider) : ICommandHandler<LoginCommand, LoginResponse>
{
    /// <summary>
    /// Обрабатывает команду входа в систему.
    /// </summary>
    public async Task<Result<LoginResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        // Получение пользователя по электронной почте.
        User? user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail(request.Email));
        }

        // Проверка соответствия пароля.
        if (!passwordManager.VerifyPassword(user.Password, request.Password))
        {
            return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
        }

        // Генерация JWT токенов
        string? token = jwtTokenProvider.Generate(user);
        string? refresh = jwtTokenProvider.GenerateRefreshToken();

        user.UpdateToken(refresh);

        // Сохранение изменений в базе данных.
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Формирование ответа.
        return new LoginResponse(user.Id, user.Email, user.Role.ToString(), token, refresh);
    }
}
