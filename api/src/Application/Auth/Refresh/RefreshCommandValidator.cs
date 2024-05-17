namespace Application.Auth.Refresh;

/// <summary>
/// Валидатор команды <see cref="RefreshCommand"/>
/// </summary>
internal sealed class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RefreshCommandValidator"/>.
    /// </summary>
    public RefreshCommandValidator()
    {
        RuleFor(c => c.RefreshToken)
            .NotEmpty().WithMessage("Необходимо указать refresh токен");

        RuleFor(c => c.AccessToken)
            .NotEmpty().WithMessage("Необходимо указать access токен.");
    }
}
