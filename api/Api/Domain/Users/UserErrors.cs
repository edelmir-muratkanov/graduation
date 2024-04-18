using Api.Shared.Models;

namespace Api.Domain.Users;

public static class UserErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        "User.EmailNotUnique",
        "Пользовать с таким email уже существует");

    public static Error NotFoundByEmail(string email) => Error.NotFound(
        "User.NotFoundByEmail",
        $"Пользователь с email = '{email}' не был найден");

    public static readonly Error InvalidCredentials = Error.Problem(
        "User.InvalidCredentials",
        "Неверные данные");
}