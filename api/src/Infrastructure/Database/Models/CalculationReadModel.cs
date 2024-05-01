using Domain.Calculation;

namespace Infrastructure.Database.Models;

internal sealed class CalculationReadModel
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectReadModel Project { get; set; }
    public Guid MethodId { get; set; }
    public MethodReadModel Method { get; set; }

    public Belonging? Belonging => Items.Any()
        ? new Belonging((double)1 / Items.Count() * Items.Sum(i => i.Belonging.Degree))
        : null;

    public IEnumerable<CalculationItemReadModel> Items { get; set; }
}
