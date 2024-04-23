namespace Infrastructure.Database.Models;

internal sealed class ProjectMethodReadModel
{
    public Guid ProjectId { get; set; }
    public Guid MethodId { get; set; }
    public MethodReadModel Method { get; set; }
}