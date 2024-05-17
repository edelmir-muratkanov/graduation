namespace Api.Contracts.Method;

/// <summary>
/// Представляет группу значений с минимальным, средним и максимальным геолого-физическими значениями.
/// </summary>
public record ValueGroup
{
    /// <summary>
    /// Минимальное значение.
    /// </summary>
    public required double Min { get; init; }

    /// <summary>
    /// Среднее значение.
    /// </summary>
    public required double Avg { get; init; }

    /// <summary>
    /// Максимальное значение.
    /// </summary>
    public required double Max { get; init; }
}

/// <summary>
/// Представляет запрос на добавление параметров метода.
/// </summary>
public record AddMethodParametersRequest
{
    /// <summary>
    /// Идентификатор свойства.
    /// </summary>
    public required Guid PropertyId { get; init; }

    /// <summary>
    /// Группа значений для первого параметра.
    /// </summary>
    public ValueGroup? First { get; init; }

    /// <summary>
    /// Группа значений для второго параметра.
    /// </summary>
    public ValueGroup? Second { get; init; }
}
