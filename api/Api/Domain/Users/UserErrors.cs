using Api.Shared.Models;

namespace Api.Domain.Entities;

public static class UserErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        "User.EmailNotUnique",
        "Пользовать с таким email уже существует");
}