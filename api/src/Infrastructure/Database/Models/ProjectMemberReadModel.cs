namespace Infrastructure.Database.Models;

/// <summary>
/// Модель участника проекта для операций чтения
/// </summary>
internal class ProjectMemberReadModel
{
    public Guid ProjectId { get; set; }
    public Guid MemberId { get; set; }
    public UserReadModel Member { get; set; }
}
