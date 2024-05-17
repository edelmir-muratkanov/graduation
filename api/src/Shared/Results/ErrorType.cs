namespace Shared.Results;

/// <summary>
/// Определяет типы ошибок, которые могут возникнуть в приложении.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Ошибка общего характера.
    /// </summary>
    Failure = 0,

    /// <summary>
    /// Ошибка валидации данных.
    /// </summary>
    Validation = 1,

    /// <summary>
    /// Проблема, возникающая во время выполнения.
    /// </summary>
    Problem = 2,

    /// <summary>
    /// Конфликт данных или состояний.
    /// </summary>
    Conflict = 3,

    /// <summary>
    /// Указанный ресурс не найден.
    /// </summary>
    NotFound = 4,

    /// <summary>
    /// Запрещенное действие или доступ.
    /// </summary>
    Forbidden = 5
}
