namespace Application.Method.Update;

/// <summary>
/// Валидатор для команды <see cref="UpdateMethodCommand"/>.
/// </summary>
internal sealed class UpdateMethodCommandValidator : AbstractValidator<UpdateMethodCommand>
{
    public UpdateMethodCommandValidator()
    {
        // Правило для проверки наличия идентификатора метода
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Идентификатор метода не может быть пустым.");

        // Правило для проверки длины имени метода
        RuleFor(c => c.Name)
            .MaximumLength(255).WithMessage("Длина названия метода не может превышать 255 символов.");

        // Правило для проверки валидности перечисления типов сборщиков
        RuleFor(c => c.CollectorTypes)
            .ForEach(ct =>
                ct.IsInEnum().WithMessage("Недопустимый тип коллектора."));
    }
}
