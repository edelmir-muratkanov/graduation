using System.Linq.Expressions;
using Domain.Calculation;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с вычислениями.
/// </summary>
internal sealed class CalculationRepository(ApplicationWriteDbContext dbContext) : ICalculationRepository
{
    /// <inheritdoc />
    public async Task<Calculation?> GetOne(
        Expression<Func<Calculation, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Calculations
            .Include(c => c.Items)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<Calculation>> Get(
        Expression<Func<Calculation, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Calculations
            .Include(c => c.Items)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }


    /// <inheritdoc />
    public void Insert(Calculation calculation)
    {
        dbContext.Calculations.Add(calculation);
    }

    /// <inheritdoc />
    public void Update(Calculation calculation)
    {
        dbContext.Calculations.Update(calculation);
    }

    /// <inheritdoc />
    public void Remove(Calculation calculation)
    {
        dbContext.Calculations.Remove(calculation);
    }
}
