namespace Application.Auth.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(AuthErrorCodes.Register.MissingEmail)
            .EmailAddress().WithErrorCode(AuthErrorCodes.Register.InvalidEmail);

        RuleFor(c => c.Password)
            .NotEmpty().WithErrorCode(AuthErrorCodes.Register.MissingPassword)
            .MinimumLength(6).WithErrorCode(AuthErrorCodes.Register.ShortPassword);
    }
}
