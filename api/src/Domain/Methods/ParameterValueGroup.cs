namespace Domain.Methods;

/// <summary>
///  Представляет группу значений параметра метода.
/// </summary>
/// <param name="Min">Минимальное геолого-физическое значение параметра</param>
/// <param name="Avg">Геолого-физическое значение параметра</param>
/// <param name="Max">Максимальное геолого-физическое значение параметра</param>
public record ParameterValueGroup(double Min, double Avg, double Max);
