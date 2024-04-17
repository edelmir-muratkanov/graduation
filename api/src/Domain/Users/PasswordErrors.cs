using Domain.Shared;

namespace Domain.Users;

public static class PasswordErrors
{
    public static readonly Error Empty = new(
        "Password.Empty",
        "Password is empty");

    public static readonly Error TooShort = new(
        "Password.TooShort",
        "Password is too short");

    public static readonly Error TooLong = new(
        "Password.TooLong",
        "Password is too long");
}