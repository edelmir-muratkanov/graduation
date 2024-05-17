namespace Application.Project.RemoveMember;

/// <summary>
/// Валидатор для команды <see cref="RemoveProjectMemberCommand"/>.
/// </summary>
internal sealed class RemoveProjectMemberCommandValidator : AbstractValidator<RemoveProjectMemberCommand>
{
    public RemoveProjectMemberCommandValidator()
    {
        // Правило для проверки наличия идентификатора проекта
        RuleFor(c => c.ProjectId)
            .NotEmpty().WithMessage("Идентификатор проекта не может быть пустым.");


        // Правило для проверки наличия идентификатора участника
        RuleFor(c => c.MemberId)
            .NotEmpty().WithMessage("Идентификатор участника не может быть пустым.");
    }
}
