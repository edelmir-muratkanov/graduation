namespace Application.Property.GetById;

/// <summary>
/// Валидатор для запроса <see cref="GetPropertyByIdQuery"/>
/// </summary>
internal class GetPropertyByIdQueryValidator : AbstractValidator<GetPropertyByIdQuery>
{
    public GetPropertyByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Идентификатор свойства не может быть пустым.");
    }
}
