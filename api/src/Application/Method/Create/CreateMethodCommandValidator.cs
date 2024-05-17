namespace Application.Method.Create;

/// <summary>
/// Валидатор для команды <see cref="CreateMethodCommand"/>.
/// </summary>
internal sealed class CreateMethodCommandValidator : AbstractValidator<CreateMethodCommand>
{
    public CreateMethodCommandValidator()
    {
        // Правила валидации для названия метода
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название метода не должно быть пустым.")
            .MaximumLength(255).WithMessage("Название метода не должно превышать 255 символов.");

        // Правила валидации для типов коллектора
        RuleFor(c => c.CollectorTypes)
            .NotEmpty().WithMessage("Список типов коллектора не должен быть пустым.")
            .ForEach(ct =>
                ct.IsInEnum().WithMessage("Недопустимый тип коллектора."));

        // Правила валидации для параметров метода
        RuleFor(c => c.Parameters)
            .NotEmpty().WithMessage("Список параметров метода не должен быть пустым.")
            .ForEach(mp => mp
                .NotEmpty().WithMessage("Параметр метода не должен быть пустым."));

        // Правила валидации для каждого параметра метода
        RuleForEach(c => c.Parameters)
            .ChildRules(mp =>
            {
                mp.RuleFor(p => p.PropertyId)
                    .NotEmpty().WithMessage("Идентификатор свойства параметра не должен быть пустым.");

                mp.RuleFor(p => p.FirstParameters)
                    .NotEmpty().WithMessage(
                        "Значения первого параметра не должны быть пустыми при отсутствии второго параметра.")
                    .When(p => p.SecondParameters is null);

                mp.RuleFor(p => p.SecondParameters)
                    .NotEmpty().WithMessage(
                        "Значения второго параметра не должны быть пустыми при отсутствии первого параметра.")
                    .When(p => p.FirstParameters is null);
            });
    }
}
