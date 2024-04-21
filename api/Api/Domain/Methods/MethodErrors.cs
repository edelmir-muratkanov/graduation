using Api.Shared.Models;

namespace Api.Domain.Methods;

public static class MethodErrors
{
    public static readonly Error NameNotUnique =
        Error.Conflict("Method.NameNotUnique", "Метод с таким именем уже существует");

    public static readonly Error NotFound = Error.NotFound("Method.NotFound", "Метод не найден");

    public static readonly Error InvalidProperty =
        Error.NotFound("MethodParameter.InvalidProperty", "Свойство параметра не найдено");
}