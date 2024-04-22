namespace Domain.Methods;

public static class MethodErrors
{
    public static readonly Error NameNotUnique =
        Error.Conflict("Method.NameNotUnique", "Метод с таким именем уже существует");

    public static readonly Error NotFound = Error.NotFound("Method.NotFound", "Метод не найден");

    public static readonly Error DuplicateParameters = Error.Conflict(
        "Method.DuplicateParameters",
        "Метод уже содержит предоставленный параметр");

    public static readonly Error NotFoundParameter = Error.NotFound(
        "Method.NotFoundParameter", "Метод не содержит указанный параметр");
}