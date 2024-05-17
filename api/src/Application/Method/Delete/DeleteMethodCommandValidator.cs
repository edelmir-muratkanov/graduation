namespace Application.Method.Delete;

/// <summary>
/// Валидатор для команды удаления метода.
/// </summary>
internal sealed class DeleteMethodCommandValidator : AbstractValidator<DeleteMethodCommand>
{
    public DeleteMethodCommandValidator()
    {
        // Правило проверки идентификатора метода
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Идентификатор метода не должен быть пустым.");
    }
}
