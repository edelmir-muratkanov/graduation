namespace Domain.Methods;

/// <summary>
/// Group of parameters used to calculate belonging degree to the method 
/// </summary>
/// <param name="Min">Minimum x</param>
/// <param name="Avg">X</param>
/// <param name="Max">Maximum x</param>
public record ParameterValueGroup(double Min, double Avg, double Max);
