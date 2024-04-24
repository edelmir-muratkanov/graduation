using Domain.Calculation;

namespace Infrastructure.Database.Models;

internal sealed class CalculationReadModel
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectReadModel Project { get; set; }
    public Guid MethodId { get; set; }
    public MethodReadModel Method { get; set; }
    public Belonging Belonging { get; set; }
    public IEnumerable<CalculationItemReadModel> Items { get; set; }
}
