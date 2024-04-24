namespace Domain.Methods;

public static class MethodParameterErrors
{
    public static readonly Error InvalidProperty =
        Error.NotFound("MethodParameter.InvalidProperty", "Свойство параметра не найдено");

    public static readonly Error MissingFirstAndSecond = Error.NotFound(
        "MethodParameter.MissingFirstAndSecond",
        "Пустые значения минимальное x1, x1, максимальное x1; минимальное x2, x2, максимальное x2");
}
