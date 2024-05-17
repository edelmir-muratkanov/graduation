namespace Application.Property.Delete;

/// <summary>
/// Валидатор для команды удаления свойства.
/// </summary>
internal sealed class DeletePropertyCommandValidator : AbstractValidator<DeletePropertyCommand>
{
    public DeletePropertyCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Идентификатор свойства не может быть пустым.");
    }
}
