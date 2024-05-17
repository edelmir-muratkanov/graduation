namespace Infrastructure.Database.Models;

/// <summary>
/// Модель метода проекта для операций чтения
/// </summary>
internal class ProjectMethodReadModel
{
    public Guid ProjectId { get; set; }
    public Guid MethodId { get; set; }
    public MethodReadModel Method { get; set; }
}
