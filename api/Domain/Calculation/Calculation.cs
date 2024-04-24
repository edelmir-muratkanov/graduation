namespace Domain.Calculation;

public class Calculation : Entity
{
    private readonly List<CalculationItem> _items = [];

    private Calculation() { }

    private Calculation(Guid projectId, Guid methodId)
    {
        ProjectId = projectId;
        MethodId = methodId;
    }

    public Guid ProjectId { get; private set; }
    public Guid MethodId { get; private set; }

    public Belonging? Belonging =>
        _items.Count != 0
            ? new Belonging((double)1 / _items.Count * _items
                .Sum(i => i.Belonging.Degree))
            : null;

    public IEnumerable<CalculationItem> Items => _items.ToList();

    public static Calculation Create(Guid projectId, Guid methodId)
    {
        var calculation = new Calculation(projectId, methodId);
        return calculation;
    }

    public Result AddItem(string propertyName, Belonging belonging)
    {
        if (_items.Any(i => i.PropertyName == propertyName))
        {
            return Result.Failure(CalculationErrors.DuplicateItems);
        }

        var item = CalculationItem.Create(Id, propertyName, belonging);

        _items.Add(item);

        return Result.Success();
    }

    public Result RemoveItem(string propertyName)
    {
        CalculationItem? item = _items.FirstOrDefault(i => i.PropertyName == propertyName);
        if (item is null)
        {
            return Result.Failure(CalculationErrors.ItemNotFound);
        }

        _items.Remove(item);

        return Result.Success();
    }
}
