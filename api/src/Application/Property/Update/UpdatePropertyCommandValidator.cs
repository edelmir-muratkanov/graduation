namespace Application.Property.Update;

/// <summary>
/// Валидатор для команды <see cref="UpdatePropertyCommand"/>
/// </summary>
internal sealed class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Идентификатор свойства не может быть пустым.");
    }
}
