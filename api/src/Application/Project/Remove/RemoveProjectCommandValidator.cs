namespace Application.Project.Remove;

/// <summary>
/// Валидатор для команды <see cref="RemoveProjectCommand"/>.
/// </summary>
internal sealed class RemoveProjectCommandValidator : AbstractValidator<RemoveProjectCommand>
{
    public RemoveProjectCommandValidator()
    {
        // Правило для проверки наличия идентификатора проекта
        RuleFor(c => c.ProjectId)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");
    }
}
