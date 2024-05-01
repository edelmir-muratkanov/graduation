namespace Domain.Users;

public static class UserErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        "User.EmailNotUnique",
        "Пользовать с таким email уже существует");

    public static readonly Error InvalidCredentials = Error.Problem(
        "User.InvalidCredentials",
        "Неверные данные");

    public static readonly Error Unauthorized = Error.Problem("User.Unauthorized", "Не авторизован");

    public static Error NotFoundByEmail(string email)
    {
        return Error.NotFound(
            "User.NotFoundByEmail",
            $"Пользователь с email = '{email}' не был найден");
    }
}
