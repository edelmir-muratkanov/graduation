namespace Domain.Calculation;

/// <summary>
/// Представляет степень принадлежности.
/// </summary>
public sealed record Belonging
{
    public const string NotApplicable = "Не пременим";
    public const string NotSuitable = "Не благоприятен для применения";
    public const string LowEfficiency = "Применим с низкой эффективностью";
    public const string Applicable = "Применим";
    public const string Favorable = "Благоприятен для применения";

    /// <summary>
    /// Закрытый конструктор без параметров, предотвращающий создание экземпляров класса за его пределами.
    /// </summary>
    private Belonging() { }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Belonging"/> на основе указанной степени принадлежности.
    /// </summary>
    /// <param name="degree">Степень принадлежности.</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается когда степень в недопустимых пределах</exception>
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

    /// <summary>
    /// Числовое представление степени принадлежности
    /// </summary>
    public double Degree { get; }

    /// <summary>
    /// Текстовое представление степени принадлежности
    /// </summary>
    public string Status { get; }
}
