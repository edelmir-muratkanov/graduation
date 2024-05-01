namespace Application.Project.RemoveMember;

internal sealed class RemoveProjectMemberCommandValidator : AbstractValidator<RemoveProjectMemberCommand>
{
    public RemoveProjectMemberCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotEmpty();

        RuleFor(c => c.MemberId)
            .NotEmpty();
    }
}
