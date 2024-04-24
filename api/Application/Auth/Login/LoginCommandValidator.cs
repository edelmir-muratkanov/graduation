namespace Application.Auth.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(AuthErrorCodes.Login.MissingEmail)
            .EmailAddress().WithErrorCode(AuthErrorCodes.Login.InvalidEmail);

        RuleFor(c => c.Password)
            .NotEmpty().WithErrorCode(AuthErrorCodes.Login.MissingPassword)
            .MinimumLength(6).WithErrorCode(AuthErrorCodes.Login.ShortPassword);
    }
}
