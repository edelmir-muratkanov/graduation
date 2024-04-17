using Domain.Shared;

namespace Domain.Users;

public sealed record Password
{
    public const int MinLength = 6;
    public const int MaxLength = 255;

    private Password()
    {
    }

    private Password(string value) => Value = value;
    public string Value { get; private set; }

    public static Result<Password> Create(string password) =>
        Result.Create(password)
            .Ensure(
                e => !string.IsNullOrWhiteSpace(e),
                PasswordErrors.Empty)
            .Ensure(
                e => e.Length >= MinLength,
                PasswordErrors.TooShort)
            .Ensure(
                e => e.Length <= MaxLength,
                PasswordErrors.TooLong)
            .Map(e => new Password(e));
}