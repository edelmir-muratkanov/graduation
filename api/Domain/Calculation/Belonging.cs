namespace Domain.Calculation;

public sealed record Belonging
{
    public const string NotApplicable = "Не пременим";
    public const string NotSuitable = "Не благоприятен для применения";
    public const string LowEfficiency = "Применим с низкой эффективностью";
    public const string Applicable = "Применим";
    public const string Favorable = "Благоприятен для применения";

    private Belonging() { }

    public Belonging(double degree)
    {
        Status = degree switch
        {
            >= -1 and < -0.75 => NotApplicable,
            >= -0.75 and < -0.25 => NotSuitable,
            >= -0.25 and < 0.25 => LowEfficiency,
            >= 0.25 and < 0.75 => Applicable,
            >= 0.75 and <= 1 => Favorable,
            _ => throw new ArgumentOutOfRangeException(nameof(degree))
        };

        Degree = degree;
    }

    public double Degree { get; }

    public string Status { get; }
}
