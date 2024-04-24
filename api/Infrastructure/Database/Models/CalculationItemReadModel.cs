using Domain.Calculation;

namespace Infrastructure.Database.Models;

public class CalculationItemReadModel
{
    public Guid Id { get; set; }
    public Guid CalculationId { get; set; }
    public string PropertyName { get; set; }
    public Belonging Belonging { get; set; }
}
