namespace Application.Auth;

public static class AuthErrorCodes
{
    public static class Register
    {
        public const string MissingEmail = nameof(MissingEmail);
        public const string InvalidEmail = nameof(InvalidEmail);

        public const string MissingPassword = nameof(MissingPassword);
        public const string ShortPassword = nameof(ShortPassword);
    }

    public static class Login
    {
        public const string MissingEmail = nameof(MissingEmail);
        public const string InvalidEmail = nameof(InvalidEmail);

        public const string MissingPassword = nameof(MissingPassword);
        public const string ShortPassword = nameof(ShortPassword);
    }
}