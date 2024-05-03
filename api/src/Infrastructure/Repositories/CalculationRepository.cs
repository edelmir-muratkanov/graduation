using System.Linq.Expressions;
using Domain.Calculation;

namespace Infrastructure.Repositories;

internal sealed class CalculationRepository(ApplicationWriteDbContext dbContext) : ICalculationRepository
{
    public async Task<Calculation?> GetOne(
        Expression<Func<Calculation, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Calculations
            .Include(c => c.Items)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<List<Calculation>> Get(
        Expression<Func<Calculation, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Calculations
            .Include(c => c.Items)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }


    public void Insert(Calculation calculation)
    {
        dbContext.Calculations.Add(calculation);
    }


    public void Update(Calculation calculation)
    {
        dbContext.Calculations.Update(calculation);
    }

    public void Remove(Calculation calculation)
    {
        dbContext.Calculations.Remove(calculation);
    }
}
