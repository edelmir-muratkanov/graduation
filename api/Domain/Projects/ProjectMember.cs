namespace Domain.Projects;

public record ProjectMember(Guid ProjectId, Guid MemberId)
{
    public Guid ProjectId { get; set; } = ProjectId;
    public Guid MemberId { get; set; } = MemberId;
}