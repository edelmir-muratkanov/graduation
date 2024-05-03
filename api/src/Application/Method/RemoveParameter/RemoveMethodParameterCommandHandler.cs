using Domain.Calculation;
using Domain.Methods;
using Domain.Projects;

namespace Application.Method.RemoveParameter;

internal class RemoveMethodParameterCommandHandler(
    IMethodRepository methodRepository,
    ICalculationService calculationService,
    ICalculationRepository calculationRepository,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveMethodParameterCommand>
{
    public async Task<Result> Handle(RemoveMethodParameterCommand request, CancellationToken cancellationToken)
    {
        Domain.Methods.Method? method = await methodRepository.GetByIdAsync(request.MethodId, cancellationToken);

        if (method is null)
        {
            return Result.Failure(MethodErrors.NotFound);
        }

        method.RemoveParameter(request.ParameterId);

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
