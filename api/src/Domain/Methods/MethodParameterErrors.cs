namespace Domain.Methods;

/// <summary>
/// Статический класс, содержащий ошибки, связанные с параметрами методов.
/// </summary>
public static class MethodParameterErrors
{
    /// <summary>
    /// Ошибка, указывающая на то, что свойство параметра не найдено.
    /// </summary>
    public static readonly Error InvalidProperty =
        Error.NotFound("MethodParameter.InvalidProperty", "Свойство параметра не найдено");

    /// <summary>
    /// Ошибка, указывающая на отсутствие значений первой и второй групп параметра.
    /// </summary>
    public static readonly Error MissingFirstAndSecond = Error.NotFound(
        "MethodParameter.MissingFirstAndSecond",
        "Пустые значения минимальное x1, x1, максимальное x1; минимальное x2, x2, максимальное x2");
}
