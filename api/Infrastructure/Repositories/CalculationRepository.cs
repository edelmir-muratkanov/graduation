using Domain.Calculation;

namespace Infrastructure.Repositories;

internal sealed class CalculationRepository(ApplicationWriteDbContext dbContext) : ICalculationRepository
{
    public async Task<Calculation?> GetByIdAsync(Guid calculationId, CancellationToken cancellationToken)
    {
        return await dbContext.Calculations.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == calculationId, cancellationToken);
    }

    public async Task<Calculation?> GetByProjectAndMethodAsync(Guid projectId, Guid methodId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Calculations.Include(c => c.Items)
            .FirstOrDefaultAsync(
                c => c.ProjectId == projectId && c.MethodId == methodId,
                cancellationToken);
    }

    public async Task<bool> Exists(Guid projectId, Guid methodId, CancellationToken cancellationToken)
    {
        return await dbContext.Calculations.AnyAsync(
            c => c.ProjectId == projectId && c.MethodId == methodId,
            cancellationToken);
    }

    public void Insert(Calculation calculation)
    {
        dbContext.Calculations.Add(calculation);
    }

    public void InsertRange(IEnumerable<Calculation> calculations)
    {
        dbContext.Calculations.AddRange(calculations);
    }

    public void Update(Calculation calculation)
    {
        dbContext.Calculations.Update(calculation);
    }
}
