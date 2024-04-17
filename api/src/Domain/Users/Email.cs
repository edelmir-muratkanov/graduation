using Domain.Shared;

namespace Domain.Users;

public sealed record Email
{
    public const int MaxLength = 255;

    private Email()
    {
    }

    private Email(string value) => Value = value;

    public string Value { get; private set; }

    public static Result<Email> Create(string email) =>
        Result.Create(email)
            .Ensure(
                e => !string.IsNullOrWhiteSpace(e),
                EmailErrors.Empty)
            .Ensure(
                e => e.Length <= MaxLength,
                EmailErrors.TooLong)
            .Ensure(
                e => e.Split('@').Length == 2,
                EmailErrors.InvalidFormat)
            .Map(e => new Email(e));
}