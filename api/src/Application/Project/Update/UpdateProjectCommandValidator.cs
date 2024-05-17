namespace Application.Project.Update;

/// <summary>
/// Валидатор для команды <see cref="UpdateProjectCommand"/>.
/// </summary>
internal sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        // Правило для проверки наличия идентификатора проекта
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");

        // Правила для проверки максимальной длины полей
        RuleFor(c => c.Name)
            .MaximumLength(255).WithMessage("Длина названия проекта не может превышать 255 символов.");

        RuleFor(c => c.Operator)
            .MaximumLength(255).WithMessage("Длина оператора проекта не может превышать 255 символов.");

        RuleFor(c => c.Country)
            .MaximumLength(255).WithMessage("Длина страны проекта не может превышать 255 символов.");

        RuleFor(c => c.ProjectType)
            .IsInEnum().WithMessage("Указан недопустимый тип проекта.");

        RuleFor(c => c.CollectorType)
            .IsInEnum().WithMessage("Указан недопустимый тип коллектора проекта.");
    }
}
