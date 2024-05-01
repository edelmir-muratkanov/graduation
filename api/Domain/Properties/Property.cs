namespace Domain.Properties;

public class Property : AuditableEntity
{
    private Property(Guid id, string name, string unit) : base(id)
    {
        Id = id;
        Name = name;
        Unit = unit;
    }

    private Property()
    {
    }

    public string Name { get; private set; }
    public string Unit { get; private set; }

    public static Result<Property> Create(string name, string unit)
    {
        return new Property(Guid.NewGuid(), name, unit);
    }

    public Result Update(string? name, string? unit)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }

        if (!string.IsNullOrWhiteSpace(unit))
        {
            Unit = unit;
        }

        return Result.Success();
    }
}
