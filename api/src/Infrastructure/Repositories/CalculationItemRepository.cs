using Domain.Calculation;

namespace Infrastructure.Repositories;

internal sealed class CalculationItemRepository(ApplicationWriteDbContext dbContext) : ICalculationItemRepository
{
    public void Remove(CalculationItem calculationItem)
    {
        dbContext.CalculationItems.Remove(calculationItem);
    }

    public void Insert(CalculationItem calculationItem)
    {
        dbContext.CalculationItems.Add(calculationItem);
    }

    public void InsertRange(IEnumerable<CalculationItem> calculationItems)
    {
        dbContext.CalculationItems.AddRange(calculationItems);
    }

    public void RemoveRange(IEnumerable<CalculationItem> calculationItems)
    {
        dbContext.CalculationItems.RemoveRange(calculationItems);
    }
    
}
