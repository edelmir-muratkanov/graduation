using Domain.Users;

namespace Application.Auth.Refresh;

/// <summary>
/// Обработчик команды <see cref="RefreshCommand"/>
/// </summary>
/// <param name="jwtTokenProvider">Провайдер JWT-токенов.</param>
/// <param name="userRepository">Репозиторий пользователей.</param>
/// <param name="unitOfWork">Unit of Work для сохранения изменений.</param>
internal sealed class RefreshCommandHandler(
    IJwtTokenProvider jwtTokenProvider,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RefreshCommand, RefreshResponse>
{
    /// <summary>
    /// Обрабатывает запрос на обновление JWT-токенов.
    /// </summary>
    /// <param name="request">Запрос на обновление JWT-токенов.</param>
    /// <param name="cancellationToken">Токен отмены задачи.</param>
    /// <returns>Результат выполнения операции обновления JWT-токенов.</returns>
    public async Task<Result<RefreshResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        // Получение идентификатора пользователя из access-токена
        string? userId = await jwtTokenProvider.GetUserFromToken(request.AccessToken);

        // Если идентификатор пользователя не получен или неверен, возвращается ошибка "Unauthorized"
        if (userId is null || !Guid.TryParse(userId, out Guid id))
        {
            return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);
        }

        // Получение пользователя из репозитория
        User? user = await userRepository.GetByIdAsync(id, cancellationToken);

        // Если пользователь не найден или токен обновления не совпадает, возвращается ошибка "Unauthorized"
        if (user is null || user.Token != request.RefreshToken)
        {
            return Result.Failure<RefreshResponse>(UserErrors.Unauthorized);
        }

        // Генерация новых JWT-токенов
        string? access = jwtTokenProvider.Generate(user);
        string? refresh = jwtTokenProvider.GenerateRefreshToken();

        user.UpdateToken(refresh);

        // Сохранение изменений
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Возвращается успешный результат с новыми токенами
        return new RefreshResponse(access, refresh);
    }
}
