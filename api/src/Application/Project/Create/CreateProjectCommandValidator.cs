namespace Application.Project.Create;

/// <summary>
/// Валидатор для команды <see cref="CreateProjectCommand"/>.
/// </summary>
internal class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        // Правило для проверки наличия названия проекта
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название проекта не может быть пустым.");

        // Правило для проверки наличия оператора проекта
        RuleFor(c => c.Operator)
            .NotEmpty().WithMessage("Оператор проекта не может быть пустым.");

        // Правило для проверки наличия страны проекта
        RuleFor(c => c.Country)
            .NotEmpty().WithMessage("Страна проекта не может быть пустой.");

        // Правило для проверки корректности типа коллектора проекта
        RuleFor(c => c.CollectorType)
            .IsInEnum().WithMessage("Некорректный тип сборщика данных проекта.");

        // Правило для проверки корректности типа проекта
        RuleFor(c => c.ProjectType)
            .IsInEnum().WithMessage("Некорректный тип проекта.");
    }
}
