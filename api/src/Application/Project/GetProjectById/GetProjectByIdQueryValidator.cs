namespace Application.Project.GetProjectById;

/// <summary>
/// Валидатор запроса <see cref="GetProjectByIdQuery"/>
/// </summary>
public class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdQueryValidator()
    {
        // Правило для проверки наличия идентификатора проекта
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");
    }
}
