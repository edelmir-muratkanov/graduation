namespace Application.Auth.Refresh;

internal sealed class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(c => c.RefreshToken)
            .NotEmpty();

        RuleFor(c => c.AccessToken)
            .NotEmpty();
    }
}