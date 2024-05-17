namespace Domain.Methods;

/// <summary>
/// Статический класс, содержащий ошибки, связанные с методами.
/// </summary>
public static class MethodErrors
{
    /// <summary>
    /// Ошибка, указывающая на неуникальное имя метода.
    /// </summary>
    public static readonly Error NameNotUnique =
        Error.Conflict("Method.NameNotUnique", "Метод с таким именем уже существует");

    /// <summary>
    /// Ошибка, указывающая на то, что метод не найден.
    /// </summary>
    public static readonly Error NotFound = Error.NotFound("Method.NotFound", "Метод не найден");

    /// <summary>
    /// Ошибка, указывающая на дубликат параметров метода.
    /// </summary>
    public static readonly Error DuplicateParameters = Error.Conflict(
        "Method.DuplicateParameters",
        "Метод уже содержит предоставленный параметр");

    /// <summary>
    /// Ошибка, указывающая на то, что параметр метода не найден.
    /// </summary>
    public static readonly Error NotFoundParameter = Error.NotFound(
        "Method.NotFoundParameter", "Метод не содержит указанный параметр");
}
