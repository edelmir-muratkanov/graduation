namespace Infrastructure.Database.Models;

internal class ProjectMethodReadModel
{
    public Guid ProjectId { get; set; }
    public Guid MethodId { get; set; }
    public MethodReadModel Method { get; set; }
}