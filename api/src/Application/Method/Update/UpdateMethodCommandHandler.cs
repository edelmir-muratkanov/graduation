using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Method.Update;

internal sealed class UpdateMethodCommandHandler(
    IMethodRepository methodRepository,
    IProjectRepository projectRepository,
    ICalculationRepository calculationRepository,
    ICalculationService calculationService,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMethodCommand>
{
    public async Task<Result> Handle(UpdateMethodCommand request, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.Id, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        method.ChangeNameAndCollectorTypes(request.Name, request.CollectorTypes);

        List<Calculation> calculations = await calculationRepository.GetByMethodAsync(method.Id, cancellationToken);

        foreach (Calculation calculation in calculations)
        {
            Domain.Projects.Project? project = await projectRepository
                .GetByIdAsync(calculation.ProjectId, cancellationToken);

            Result result = await calculationService.Update(project!, method);

            if (result.IsFailure)
            {
                return result;
            }
        }

        methodRepository.Update(method);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
