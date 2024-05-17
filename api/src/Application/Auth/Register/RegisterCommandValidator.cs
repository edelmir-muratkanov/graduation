namespace Application.Auth.Register;

/// <summary>
/// Валидатор для команды <see cref="RegisterCommand"/>
/// </summary>
internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        // Правила валидации для адреса электронной почты
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Адрес электронной почты не должен быть пустым")
            .EmailAddress().WithMessage("Неверный формат адреса электронной почты");

        // Правила валидации для пароля
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Пароль не должен быть пустым")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов");
    }
}
