using Domain.Calculation;

namespace Application.Calculations.GetByProject;

public record CalculationItemResponse
{
    public string Name { get; set; }
    public double Ratio { get; set; }
}

public record CalculationResponse
{
    public string Name { get; set; }
    public double? Ratio { get; set; }
    public string? Applicability { get; set; }
    public List<CalculationItemResponse> Items { get; set; }
}

public record GetCalculationsByProjectQuery : IQuery<List<CalculationResponse>>
{
    public Guid ProjectId { get; set; }
}
