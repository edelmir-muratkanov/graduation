using Domain.Calculation;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация репозитория для работы с элементами вычислений.
/// </summary>
internal sealed class CalculationItemRepository(ApplicationWriteDbContext dbContext) : ICalculationItemRepository
{
    /// <inheritdoc />
    public void Remove(CalculationItem calculationItem)
    {
        dbContext.CalculationItems.Remove(calculationItem);
    }

    /// <inheritdoc />
    public void Insert(CalculationItem calculationItem)
    {
        dbContext.CalculationItems.Add(calculationItem);
    }

    /// <inheritdoc />
    public void InsertRange(IEnumerable<CalculationItem> calculationItems)
    {
        dbContext.CalculationItems.AddRange(calculationItems);
    }

    /// <inheritdoc />
    public void RemoveRange(IEnumerable<CalculationItem> calculationItems)
    {
        dbContext.CalculationItems.RemoveRange(calculationItems);
    }
    
}
