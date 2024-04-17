using Domain.Shared;

namespace Domain.Users;

public static class EmailErrors
{
    public static readonly Error Empty = new(
        "Email.Empty",
        "Email is empty");

    public static readonly Error TooLong = new(
        "Email.TooLong",
        "Email is too long");

    public static readonly Error InvalidFormat = new(
        "Email.InvalidFormat",
        "Email format is invalid");
}