namespace Application.Method.GetMethodById;

/// <summary>
/// Валидатор запроса <see cref="GetMethodByIdQuery"/>
/// </summary>
public class GetMethodByIdQueryValidator : AbstractValidator<GetMethodByIdQuery>
{
    public GetMethodByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Идентификатор метода не должен быть пустым.");
    }
}
