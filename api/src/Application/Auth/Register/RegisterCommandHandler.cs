using Domain.Users;

namespace Application.Auth.Register;

/// <summary>
/// Обработчик команды регистрации нового пользователя.
/// </summary>
/// <param name="userRepository">Репозиторий пользователей.</param>
/// <param name="unitOfWork">Unit of Work для сохранения изменений.</param>
/// <param name="passwordManager">Менеджер паролей для хеширования пароля.</param>
/// <param name="jwtTokenProvider">Провайдер токенов для генерации JWT-токенов.</param>
internal sealed class RegisterCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordManager passwordManager,
    IJwtTokenProvider jwtTokenProvider)
    : ICommandHandler<RegisterCommand, RegisterResponse>
{
    /// <summary>
    /// Обрабатывает запрос на регистрацию нового пользователя.
    /// </summary>
    /// <param name="request">Запрос на регистрацию пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения операции регистрации.</returns>
    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Проверяем, что электронная почта уникальна
        if (!await userRepository.IsEmailUniqueAsync(request.Email))
        {
            return Result.Failure<RegisterResponse>(UserErrors.EmailNotUnique);
        }

        // Хешируем пароль
        string? password = passwordManager.HashPassword(request.Password);
        // Создаем пользователя
        Result<User>? userResult = User.Create(request.Email, password);

        // Если создание пользователя не удалось, возвращаем ошибку
        if (userResult.IsFailure)
        {
            return Result.Failure<RegisterResponse>(userResult.Error);
        }

        // Генерируем токен доступа и обновляем Refresh токен
        string? token = jwtTokenProvider.Generate(userResult.Value);
        string? refresh = jwtTokenProvider.GenerateRefreshToken();

        userResult.Value.UpdateToken(refresh);

        // Вставляем пользователя в репозиторий
        userRepository.Insert(userResult.Value);
        // Сохраняем изменения
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Формируем ответ с данными о зарегистрированном пользователе и токенах
        return new RegisterResponse(
            userResult.Value.Id,
            userResult.Value.Email,
            userResult.Value.Role.ToString(),
            token,
            refresh);
    }
}
