namespace Domain.Calculation;

public interface ICalculationItemRepository
{
    void Remove(CalculationItem calculationItem);
    void Insert(CalculationItem calculationItem);
    void InsertRange(IEnumerable<CalculationItem> calculationItems);
    void RemoveRange(IEnumerable<CalculationItem> calculationItems);
}
