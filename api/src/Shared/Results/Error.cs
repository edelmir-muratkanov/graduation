namespace Shared.Results;

/// <summary>
/// Представляет ошибку с кодом, описанием и типом.
/// </summary>
/// <param name="Code">Код ошибки.</param>
/// <param name="Description">Описание ошибки.</param>
/// <param name="Type">Тип ошибки.</param>
public record Error(string Code, string Description, ErrorType Type)
{
    /// <summary>
    /// Статическое поле, представляющее отсутствие ошибки.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    /// Статическое поле, представляющее ошибку из-за предоставленного значения null.
    /// </summary>
    public static readonly Error NullValue = new("General.Null", "Null value provided", ErrorType.Failure);

    /// <summary>
    /// Создает объект <see cref="Error"/> с типом <see cref="ErrorType.Failure"/>.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <param name="description">Описание ошибки.</param>
    /// <returns>
    /// Объект <see cref="Error"/> с типом <see cref="ErrorType.Failure"/>.
    /// </returns>
    public static Error Failure(string code, string description)
    {
        return new Error(code, description, ErrorType.Failure);
    }

    /// <summary>
    /// Создает объект <see cref="Error"/> с типом <see cref="ErrorType.NotFound"/>.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <param name="description">Описание ошибки.</param>
    /// <returns>
    /// Объект <see cref="Error"/> с типом <see cref="ErrorType.NotFound"/>.
    /// </returns>
    public static Error NotFound(string code, string description)
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    /// <summary>
    /// Создает объект <see cref="Error"/> с типом <see cref="ErrorType.Problem"/>.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <param name="description">Описание ошибки.</param>
    /// <returns>
    /// Объект <see cref="Error"/> с типом <see cref="ErrorType.Problem"/>.
    /// </returns>
    public static Error Problem(string code, string description)
    {
        return new Error(code, description, ErrorType.Problem);
    }

    /// <summary>
    /// Создает объект <see cref="Error"/> с типом <see cref="ErrorType.Conflict"/>.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <param name="description">Описание ошибки.</param>
    /// <returns>
    /// Объект <see cref="Error"/> с типом <see cref="ErrorType.Conflict"/>.
    /// </returns>
    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict);
    }

    /// <summary>
    /// Создает объект <see cref="Error"/> с типом <see cref="ErrorType.Forbidden"/>.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <param name="description">Описание ошибки.</param>
    /// <returns>
    /// Объект <see cref="Error"/> с типом <see cref="ErrorType.Forbidden"/>.
    /// </returns>
    public static Error Forbidden(string code, string description)
    {
        return new Error(code, description, ErrorType.Forbidden);
    }
}
