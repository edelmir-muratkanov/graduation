namespace Domain.Calculation;

public class CalculationItem : Entity
{
    private CalculationItem() { }

    private CalculationItem(Guid id, Guid calculationId, string propertyName, Belonging degree) : base(id)
    {
        CalculationId = calculationId;
        PropertyName = propertyName;
        Belonging = degree;
    }

    public Guid CalculationId { get; private set; }
    public string PropertyName { get; private set; }
    public Belonging Belonging { get; private set; }

    public static CalculationItem Create(Guid calculationId, string propertyName, Belonging degree)
    {
        var item = new CalculationItem(Guid.NewGuid(), calculationId, propertyName, degree);

        return item;
    }

    public void UpdateBelonging(Belonging belonging)
    {
        Belonging = belonging;
    }
}
