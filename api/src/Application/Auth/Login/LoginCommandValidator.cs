namespace Application.Auth.Login;

/// <summary>
/// Валидатор команды входа в систему.
/// </summary>
internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        // Правила валидации для адреса электронной почты.
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Адрес электронной почты обязателен для заполнения")
            .EmailAddress().WithMessage("Недопустимый формат адреса электронной почты");


        // Правила валидации для пароля.
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Пароль обязателен для заполнения")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов");
    }
}
