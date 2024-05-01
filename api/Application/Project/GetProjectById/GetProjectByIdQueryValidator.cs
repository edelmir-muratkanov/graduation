namespace Application.Project.GetProjectById;

public class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithErrorCode(ProjectErrorCodes.GetById.MissingId);
    }
}
